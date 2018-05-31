using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureTmsAttribute : BaseAllureAttribute
    {
        [Obsolete("Url is configuredi allureConfig.json", false)]
        public AllureTmsAttribute(string name, string url)
        {
            TmsLink = new Link { name = name, type = "tms", url = name };
        }

        public AllureTmsAttribute(string name)
        {
            TmsLink = new Link { name = name, type = "tms", url = name };
        }

        private Link TmsLink { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            //Fix for NUnit.Retry
            if (!TmsLink.url.Equals(TmsLink.name)) TmsLink.url = TmsLink.name;

            Allure.UpdateTestCase(x => x.links.Add(TmsLink));
            base.AfterTest(test);
        }
    }
}