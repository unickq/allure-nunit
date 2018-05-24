using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureIssueAttribute : BaseAllureAttribute
    {
        public AllureIssueAttribute(string name, string url = null)
        {
            IssueLink = new Link {name = name, type = "issue", url = url};
        }

        private Link IssueLink { get; }

        public override ActionTargets Targets => ActionTargets.Test;

        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.links.Add(IssueLink));
            base.AfterTest(test);
        }
    }
}