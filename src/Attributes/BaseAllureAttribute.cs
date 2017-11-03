using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    public abstract class BaseAllureAttribute : Attribute, ITestAction
    {
        protected static AllureLifecycle Allure => AllureLifecycle.Instance;

        public virtual void BeforeTest(ITest test)
        {
            var currentAction = (ITestAction) this;
            var testActions = GetCurrentActions(test);

            if (testActions.Last() == currentAction)
            {
                var testResult = new TestResult
                {
                    uuid = test.Id,
                    name = test.MethodName,
                    fullName = test.FullName,
                    labels = new List<Label>
                    {
                        Label.Suite(test.ClassName),
                        Label.Thread(),
                        Label.Host()
                    }
                };
                Allure.StartTestCase(testResult);
            }
        }

        public virtual void AfterTest(ITest test)
        {
            var currentAction = (ITestAction) this;
            var testActions = GetCurrentActions(test);

            if (testActions.Last() == currentAction)
            {
                Allure.UpdateTestCase(x => x.statusDetails = new StatusDetails
                {
                    message = TestContext.CurrentContext.Result.Message,
                    trace = TestContext.CurrentContext.Result.StackTrace
                });
                Allure.StopTestCase(x => x.status = GetNunitStatus(TestContext.CurrentContext.Result.Outcome.Status));
                Allure.WriteTestCase(test.Id);
            }
        }

        public abstract ActionTargets Targets { get; }

        private ITestAction[] GetCurrentActions(ITest test)
        {
            var props = test.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            return (ITestAction[]) props[props.Length - 1].GetValue(test);
        }

        private Status GetNunitStatus(TestStatus status)
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
    }
}