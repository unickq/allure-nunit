using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureTagAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureTagAttribute(string value)
        {
            Value = value;
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.labels.Add(Label.Tag(Value)));

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}