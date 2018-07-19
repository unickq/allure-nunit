using System;
using Allure.Commons;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllureIssueAttribute : NUnitAttribute
    {
        public AllureIssueAttribute(string name, string url = null)
        {
            IssueLink = new Link { name = name, type = "issue", url = url };
        }

        internal Link IssueLink { get; }
    }
}