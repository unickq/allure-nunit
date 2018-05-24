using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    [Obsolete("Use Test(Description = \"Test example description\")")]
    public class AllureTestAttribute : BaseAllureAttribute
    {
        public AllureTestAttribute(string description = "")
        {
            Description = description;
        }

        private string Description { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            if (!string.IsNullOrEmpty(Description)) Allure.UpdateTestCase(x => x.description = Description);
            base.AfterTest(test);
        }
    }
}