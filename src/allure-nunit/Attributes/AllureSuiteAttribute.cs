using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureSuiteAttribute : AllureTestCaseAttribute
    {
        public AllureSuiteAttribute(string suite)
        {
            Suite = suite;
        }

        internal string Suite { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            testResult.labels.Add(Label.Suite(Suite));
        }
    }
}