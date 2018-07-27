using System;
using System.Collections.Generic;
using System.Linq;
using Allure.Commons;
using Newtonsoft.Json.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using TestResult = Allure.Commons.TestResult;

namespace NUnit.Allure.Core
{
    public sealed class AllureNUnitHelper
    {
        private static AllureLifecycle AllureLifecycle => AllureLifecycle.Instance;

        private readonly ITest _test;

        private string _containerGuid;
        private string _testResultGuid;
        private string _stepGuid;

        private StepResult _stepResult;
        bool _isSetupFailed;

        public AllureNUnitHelper(ITest test)
        {
            _test = test;
        }

        private void StartTestContainer()
        {
            var testFixture = GetTestFixture();
            _containerGuid = string.Concat(Guid.NewGuid().ToString(), "-tc-", testFixture.Id);
            var container = new TestResultContainer
            {
                uuid = _containerGuid,
                name = _test.FullName
            };
            AllureLifecycle.StartTestContainer(container);
        }

        private void StartTestCase()
        {
            _testResultGuid = string.Concat(Guid.NewGuid().ToString(), "-tr-", _test.Id);
            var testResult = new TestResult
            {
                uuid = _testResultGuid,
                name = _test.Name,
                historyId = _test.FullName,
                fullName = _test.FullName,
                labels = new List<Label>
                {
                    Label.Thread(),
                    Label.Host(),
                    Label.TestClass(_test.ClassName),
                    Label.TestMethod(_test.MethodName),
                    Label.Package(_test.ClassName)
                }
            };
            AllureLifecycle.StartTestCase(_containerGuid, testResult);
        }

        private TestFixture GetTestFixture()
        {
            var currentTest = _test;
            var isTestSuite = currentTest.IsSuite;
            while (isTestSuite != true)
            {
                currentTest = currentTest.Parent;
                if (currentTest is ParameterizedMethodSuite) currentTest = currentTest.Parent;
                isTestSuite = currentTest.IsSuite;
            }

            return (TestFixture) currentTest;
        }

        private IEnumerable<string> GetNUnitHelpMethods(NUnitHelpMethodType type, TestFixture fixture)
        {
            switch (type)
            {
                case NUnitHelpMethodType.SetUp:
                    return fixture.SetUpMethods.Select(m => m.Name).ToList();
                case NUnitHelpMethodType.TearDown:
                    return fixture.TearDownMethods.Select(m => m.Name).ToList();
                case NUnitHelpMethodType.OneTimeSetup:
                    return fixture.OneTimeSetUpMethods.Select(m => m.Name).ToList();
                case NUnitHelpMethodType.OneTimeTearDown:
                    return fixture.OneTimeTearDownMethods.Select(m => m.Name).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public enum NUnitHelpMethodType
        {
            SetUp,
            TearDown,
            OneTimeSetup,
            OneTimeTearDown
        }

        private void StartTestStep()
        {
            _stepGuid = string.Concat(Guid.NewGuid().ToString(), "-ts-", _test.Id);
            _stepResult = new StepResult {name = _test.Name};
            AllureLifecycle.StartStep(_stepGuid, _stepResult);
        }

        private List<FixtureResult> BuildFixtureResults(NUnitHelpMethodType type, TestFixture testFixture)
        {
            var fixtureResultsList = new HashSet<FixtureResult>();
            var testResult = TestExecutionContext.CurrentContext.CurrentResult;

            foreach (var method in GetNUnitHelpMethods(type, testFixture))
            {
                var fr = new FixtureResult {name = method};
                if (testResult.StackTrace != null && testResult.StackTrace.Contains(method))
                {
                    AllureLifecycle.UpdateTestCase(x => x.description += $"\n{method} {type} method failed\n");
                    fr.status = Status.failed;
                    fr.statusDetails.message = testResult.Message;
                    fr.statusDetails.trace = testResult.StackTrace;

                    if (type == NUnitHelpMethodType.SetUp) _isSetupFailed = true;
                }
                else
                {
                    fr.status = Status.passed;
                }

                fixtureResultsList.Add(fr);
            }

            return fixtureResultsList.ToList();
        }

        public void StopAll(bool isWrapedIntoStep)
        {
            var testFixture = GetTestFixture();

            var listSetups = BuildFixtureResults(NUnitHelpMethodType.SetUp, testFixture);
            var listTeardowns = BuildFixtureResults(NUnitHelpMethodType.TearDown, testFixture);

            var result = TestExecutionContext.CurrentContext.CurrentResult;

            if (isWrapedIntoStep)
            {
                AllureLifecycle.StopStep(step =>
                {
                    step.statusDetails = new StatusDetails
                    {
                        message = result.Message,
                        trace = result.StackTrace
                    };
                    var param = new Parameter
                    {
                        name = "Console Output",
                        value = result.Output
                    };
                    step.parameters.Add(param);

                    if (_isSetupFailed)
                    {
                        step.status = Status.skipped;
                        step.name += $" skipped because of {listSetups.Select(sm => sm.name).FirstOrDefault()} failure";
                    }
                    else
                    {
                        step.status =
                            GetNunitStatus();
                    }
                });
            }

            StopTestCase();

            StopTestContainer(listSetups, listTeardowns);
        }

        private void StopTestCase()
        {
            UpdateTestDataFromAttributes();

            AllureLifecycle.UpdateTestCase(x => x.statusDetails = new StatusDetails
            {
                message = TestContext.CurrentContext.Result.Message,
                trace = TestContext.CurrentContext.Result.StackTrace
            });

            AllureLifecycle.StopTestCase(testCase => testCase.status = GetNunitStatus());
            AllureLifecycle.WriteTestCase(_testResultGuid);
        }

        private void StopTestContainer(List<FixtureResult> listSetups, List<FixtureResult> listTeardowns)
        {
            AllureLifecycle.UpdateTestContainer(_containerGuid, cont =>
            {
                cont.befores.AddRange(listSetups);
                cont.afters.AddRange(listTeardowns);
            });
            AllureLifecycle.StopTestContainer(_containerGuid);
            AllureLifecycle.WriteTestContainer(_containerGuid);
        }

        public void StartAll(bool isWrapedIntoStep)
        {
            StartTestContainer();
            StartTestCase();
            if (isWrapedIntoStep) StartTestStep();
        }

        private static Status GetNunitStatus()
        {
            var result = TestContext.CurrentContext.Result;

            if (result.Outcome.Status != TestStatus.Passed)
            {
                var jo = JObject.Parse(AllureLifecycle.JsonConfiguration);
                var allureSection = jo["allure"];
                var config = allureSection?.ToObject<AllureExtendedConfiguration>();
                if (config?.BrokenTestData != null)
                {
                    foreach (var word in config.BrokenTestData)
                    {
                        if (result.Message.Contains(word))
                        {
                            return Status.broken;
                        }
                    }
                }

                switch (result.Outcome.Status)
                {
                    case TestStatus.Inconclusive:
                        return Status.broken;
                    case TestStatus.Skipped:
                        return Status.skipped;
                    case TestStatus.Passed:
                        return Status.passed;
                    case TestStatus.Warning:
                        return Status.broken;
                    case TestStatus.Failed:
                        return Status.failed;
                    default:
                        return Status.none;
                }
            }

            return Status.passed;
        }

        private void UpdateTestDataFromAttributes()
        {
            if (_test.Properties.ContainsKey(PropertyNames.Author))
                AllureLifecycle.UpdateTestCase(x =>
                    x.labels.Add(Label.Owner(_test.Properties.Get(PropertyNames.Author).ToString())));


            if (_test.Properties.ContainsKey(PropertyNames.Description))
                AllureLifecycle.UpdateTestCase(x =>
                    x.description += $"{_test.Properties.Get(PropertyNames.Description).ToString()}\n");


            if (_test.Properties.ContainsKey(PropertyNames.Category))
                AllureLifecycle.UpdateTestCase(x =>
                    x.labels.Add(Label.Tag(_test.Properties.Get(PropertyNames.Category).ToString())));


            var attributes = _test.Method.GetCustomAttributes<NUnitAttribute>(true).ToList();

            foreach (var attribute in attributes)
                switch (attribute)
                {
                    case AllureFeatureAttribute featureAttr:
                        foreach (var feature in featureAttr.Features)
                            AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Feature(feature)));
                        break;
                    case AllureIssueAttribute issueAttr:
                        AllureLifecycle.UpdateTestCase(x => x.links.Add(issueAttr.IssueLink));
                        break;
                    case AllureSeverityAttribute severityAttr:
                        AllureLifecycle.UpdateTestCase(
                            x => x.labels.Add(Label.Severity(severityAttr.Severity)));
                        break;
                    case AllureStoryAttribute storyAttr:
                        foreach (var story in storyAttr.Stories)
                            AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Story(story)));
                        break;
                    case AllureTagAttribute tagAttr:
                        foreach (var tag in tagAttr.Tags)
                            AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Tag(tag)));
                        break;
                    case AllureTmsAttribute tmsAttr:
                        AllureLifecycle.UpdateTestCase(x => x.links.Add(tmsAttr.TmsLink));
                        break;
                    case AllureLinkAttribute linkAttr:
                        AllureLifecycle.UpdateTestCase(x => x.links.Add(linkAttr.Link));
                        break;
                    case AllureSuiteAttribute suiteAttr:
                        AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Suite(suiteAttr.Suite)));
                        break;
                    case AllureSubSuiteAttribute subSuiteAttr:
                        AllureLifecycle.UpdateTestCase(
                            x => x.labels.Add(Label.SubSuite(subSuiteAttr.SubSuite)));
                        break;
                    case AllureOwnerAttribute ownerAttr:
                        AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Owner(ownerAttr.Owner)));
                        break;
                    case AllureEpicAttribute epicAttr:
                        AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Epic(epicAttr.Epic)));
                        break;
                    case AllureParentSuiteAttribute parentSuiteAttr:
                        AllureLifecycle.UpdateTestCase(x =>
                            x.labels.Add(Label.ParentSuite(parentSuiteAttr.ParentSuite)));
                        break;
                }
        }


        public static void WrapInStep(Action action, string stepName = "")
        {
            var id = Guid.NewGuid().ToString();
            var stepResult = new StepResult {name = stepName};
            try
            {
                AllureLifecycle.StartStep(id, stepResult);
                action.Invoke();
                AllureLifecycle.StopStep(step => stepResult.status = Status.passed);
            }
            catch (Exception e)
            {
                AllureLifecycle.StopStep(step =>
                {
                    step.statusDetails = new StatusDetails
                    {
                        message = e.Message,
                        trace = e.StackTrace
                    };
                    step.status = GetNunitStatus();
                });
                throw;
            }
        }
    }
}