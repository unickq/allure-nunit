using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureDescriptionAttribute : NUnitAttribute
    {
        public AllureDescriptionAttribute(string description, bool html = false)
        {
            TestDescription = description;
            IsHtml = html;
        }

        internal string TestDescription { get; }
        internal bool IsHtml { get; }
    }
}