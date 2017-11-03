using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureTmsAttribute : BaseAllureAttribute
    {
        private Link Link { get; }

        public AllureTmsAttribute(string name, string url)
        {
            Link = new Link {name = name, type = "tms", url = url};
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.links.Add(Link));

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}