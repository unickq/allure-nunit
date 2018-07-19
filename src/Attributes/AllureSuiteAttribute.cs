using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureSuiteAttribute : NUnitAttribute
    {
        public AllureSuiteAttribute(string suite)
        {
            Suite = suite;
        }

        internal string Suite { get; }
    }
}