using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureFeatureAttribute : BaseAllureAttribute
    {
        public AllureFeatureAttribute(params string[] feature)
        {
            Features = feature;
        }

        private string[] Features { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            foreach (var feature in Features) Allure.UpdateTestCase(x => x.labels.Add(Label.Feature(feature)));
            base.AfterTest(test);
        }
    }
}