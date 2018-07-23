using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureEpicAttribute : NUnitAttribute
    {
        public AllureEpicAttribute(string epic)
        {
            Epic = epic;
        }

        public string Epic { get; }
    }
}