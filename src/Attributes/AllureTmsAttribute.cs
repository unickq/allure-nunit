using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureTmsAttribute : BaseAllureAttribute
    {
        private Link TmsLink { get; }

        public AllureTmsAttribute(string name, string url)
        {
            TmsLink = new Link {name = name, type = "tms", url = url};
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.links.Add(TmsLink));
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}