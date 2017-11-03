using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureFeatureAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureFeatureAttribute(string story)
        {
            Value = story;
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.labels.Add(Label.Feature(Value)));

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}