using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureIssueAttribute : BaseAllureAttribute
    {
        [Obsolete("Url is configuredi in allureConfig.json", false)]
        public AllureIssueAttribute(string name, string url)
        {
            IssueLink = new Link {name = name, type = "issue", url = name};
        }

        public AllureIssueAttribute(string name)
        {
            IssueLink = new Link { name = name, type = "issue", url = name };
        }

        private Link IssueLink { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            //Fix for NUnit.Retry
            if (!IssueLink.url.Equals(IssueLink.name)) IssueLink.url = IssueLink.name;

            Allure.UpdateTestCase(x => x.links.Add(IssueLink));
            base.AfterTest(test);
        }
    }
}