using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureTagAttribute : BaseAllureAttribute
    {
        public AllureTagAttribute(params string[] tags)
        {
            Tags = tags;
        }

        private string[] Tags { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            foreach (var tag in Tags) Allure.UpdateTestCase(x => x.labels.Add(Label.Tag(tag)));
            base.AfterTest(test);
        }
    }
}