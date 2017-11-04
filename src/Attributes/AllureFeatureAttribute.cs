using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureFeatureAttribute : BaseAllureAttribute
    {
        private string[] Features { get; }

        public AllureFeatureAttribute(params string[] feature)
        {
            Features = feature;
        }

        public override void AfterTest(ITest test)
        {
            foreach (var feature in Features)
            {
                Allure.UpdateTestCase(x => x.labels.Add(Label.Feature(feature)));
            }
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}