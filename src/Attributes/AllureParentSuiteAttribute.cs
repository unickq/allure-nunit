using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureParentSuiteAttribute : NUnitAttribute
    {
        public AllureParentSuiteAttribute(string parentSuite)
        {
            ParentSuite = parentSuite;
        }

        internal string ParentSuite { get; }
    }
}