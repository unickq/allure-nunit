using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AllureSeverityAttribute : AllureTestCaseAttribute
    {
        public AllureSeverityAttribute(SeverityLevel severity = SeverityLevel.normal)
        {
            Severity = severity;
        }

        internal SeverityLevel Severity { get; }

        public override void UpdateTestResult(TestResult testCaseResult)
        {
            testCaseResult.labels.Add(Label.Severity(Severity));
        }
    }
}