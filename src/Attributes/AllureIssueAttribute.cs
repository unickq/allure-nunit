using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureIssueAttribute : BaseAllureAttribute
    {
        private Link Link { get; }

        public AllureIssueAttribute(string name, string url)
        {
            Link = new Link {name = name, type = "issue", url = url};
        }

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.links.Add(Link));
            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}