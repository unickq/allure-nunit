using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureStoryAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureStoryAttribute(string story)
        {
            Value = story;
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.labels.Add(Label.Story(Value)));
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}