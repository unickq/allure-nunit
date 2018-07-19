using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Allure.Commons;
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

        private string _stepGuid;
        private StepResult _stepResult;
        bool _isSetupFailed;

        public AllureNUnitHelper(ITest test)
        {
            _test = test;
        }

        public void StartTestContainer()
        {
            _containerGuid = Guid.NewGuid().ToString();
            var container = new TestResultContainer
            {
                uuid = _containerGuid,
                name = _test.FullName
            };
            AllureLifecycle.StartTestContainer(container);
        }

        public void StartTestCase()
        {
            var testResult = new TestResult
            {
                uuid = _test.Id,
                name = _test.Name,
                historyId = _test.FullName,
                fullName = _test.FullName,
                labels = new List<Label>
                {
                    Label.Thread(),
                    Label.Host(),
                    Label.TestClass(_test.ClassName),
                    Label.TestMethod(_test.MethodName)
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

        public void StartTestStep(bool isWrapedIntoStep)
        {
            if (isWrapedIntoStep)
            {
                _stepGuid = Guid.NewGuid().ToString();
                _stepResult = new StepResult {name = _test.Name};
                AllureLifecycle.StartStep(_stepGuid, _stepResult);
            }
        }

        public List<FixtureResult> BuildFixtureResults(NUnitHelpMethodType type, TestFixture testFixture)
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

            if (isWrapedIntoStep)
            {
                AllureLifecycle.StopStep(step =>
                {
                    step.statusDetails = new StatusDetails
                    {
                        message = TestContext.CurrentContext.Result.Message,
                        trace = TestContext.CurrentContext.Result.StackTrace
                    };
                    var param = new Parameter
                    {
                        name = "Console Output",
                        value = TestExecutionContext.CurrentContext.CurrentResult.Output
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
                            GetNunitStatus(TestContext.CurrentContext.Result.Outcome.Status);
                    }
                });
            }


            AddAttrInfoToTestCaseFromAttributes();

            AllureLifecycle.UpdateTestCase(x => x.statusDetails = new StatusDetails
            {
                message = TestContext.CurrentContext.Result.Message,
                trace = TestContext.CurrentContext.Result.StackTrace
            });


            AllureLifecycle.StopTestCase(testCase =>
                testCase.status = GetNunitStatus(TestContext.CurrentContext.Result.Outcome.Status));
            AllureLifecycle.WriteTestCase(TestContext.CurrentContext.Test.ID);


            AllureLifecycle.UpdateTestContainer(_containerGuid, cont =>
            {
                var z = _containerGuid;
                cont.befores.AddRange(listSetups);
                cont.afters.AddRange(listTeardowns);
            });


            AllureLifecycle.StopTestContainer(_containerGuid);
            AllureLifecycle.WriteTestContainer(_containerGuid);
        }

        private static Status GetNunitStatus(TestStatus status)
        {
            switch (status)
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

        private void AddAttrInfoToTestCaseFromAttributes()
        {
            if (_test.Properties.ContainsKey("Author"))
            {
                var list = _test.Properties["Author"];
                AllureLifecycle.UpdateTestCase(x => x.labels.Add(Label.Owner(list[0].ToString())));
            }

            if (_test.Properties.ContainsKey("Description"))
            {
                var list = _test.Properties["Description"];
                AllureLifecycle.UpdateTestCase(x => x.description += $"{list[0].ToString()}\n");
            }

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
                    step.status = GetNunitStatus(TestContext.CurrentContext.Result.Outcome.Status);
                });
                throw;
            }
        }
    }
}