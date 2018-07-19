using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureStoryAttribute : NUnitAttribute
    {
        public AllureStoryAttribute(params string[] story)
        {
            Stories = story;
        }

        internal string[] Stories { get; }
    }
}