using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureSeverityAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureSeverityAttribute(AllureSeverity severity = AllureSeverity.Normal)
        {
            Value = severity.ToString().ToLower();
        }


        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.labels.Add(Label.Severity(Value)));

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}