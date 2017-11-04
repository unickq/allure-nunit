using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureStoryAttribute : BaseAllureAttribute
    {
        private string[] Stories { get; }

        public AllureStoryAttribute(params string[] story)
        {
            Stories = story;
        }

        public override void AfterTest(ITest test)
        {
            foreach (var story in Stories)
            {
                Allure.UpdateTestCase(x => x.labels.Add(Label.Story(story)));
            }
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}