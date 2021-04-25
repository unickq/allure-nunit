using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureNameAttribute : NUnitAttribute
    {
        public AllureNameAttribute(string name)
        {
            TestName = name;
        }

        internal string TestName { get; }
    }
}