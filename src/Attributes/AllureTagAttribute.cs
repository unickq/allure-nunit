using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureTagAttribute : BaseAllureAttribute
    {
        private string[] Tags { get; }

        public AllureTagAttribute(params string[] tags)
        {
            Tags = tags;
        }

        public override void AfterTest(ITest test)
        {
            foreach (var tag in Tags)
            {
                Allure.UpdateTestCase(x => x.labels.Add(Label.Tag(tag)));
            }
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}