using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureOwnerAttribute : AllureTestCaseAttribute
    {
        public AllureOwnerAttribute(string owner)
        {
            Owner = owner;
        }

        internal string Owner { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            testResult.labels.Add(Label.Owner(Owner));
        }
    }
}