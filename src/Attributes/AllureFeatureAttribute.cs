using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureFeatureAttribute : NUnitAttribute
    {
        public AllureFeatureAttribute(params string[] feature)
        {
            Features = feature;
        }

        internal string[] Features { get; }
    }
}