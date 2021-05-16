using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureLabelAttribute : AllureTestCaseAttribute
    {
        public AllureLabelAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        internal string Name { get; }
        internal string Value { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            testResult.labels.Add(new Label { name = Name, value = Value });
        }
    }
}