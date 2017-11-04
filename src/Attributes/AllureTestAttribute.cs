using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureTestAttribute : BaseAllureAttribute
    {
        private string Description { get; }

        public AllureTestAttribute(string description = "")
        {
            Description = description;
        }

        public override void AfterTest(ITest test)
        {
            if (!string.IsNullOrEmpty(Description)) Allure.UpdateTestCase(x => x.description = Description);
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}