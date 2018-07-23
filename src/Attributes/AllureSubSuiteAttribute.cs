using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureSubSuiteAttribute : NUnitAttribute
    {
        public AllureSubSuiteAttribute(string subSuite)
        {
            SubSuite = subSuite;
        }

        internal string SubSuite { get; }
    }
}