using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureStoryAttribute : AllureTestCaseAttribute
    {
        public AllureStoryAttribute(params string[] story)
        {
            Stories = story;
        }

        internal string[] Stories { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            foreach (var story in Stories)
                testResult.labels.Add(Label.Story(story));
        }
    }
}