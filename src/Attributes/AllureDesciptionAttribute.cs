using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureDesciptionAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureDesciptionAttribute(string story)
        {
            Value = story;
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.description = Value);

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}