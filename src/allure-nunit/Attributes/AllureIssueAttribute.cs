﻿using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureIssueAttribute : AllureTestCaseAttribute
    {
        public AllureIssueAttribute(string name, string url)
        {
            IssueLink = new Link {name = name, type = "issue", url = url};
        }

        public AllureIssueAttribute(string name)
        {
            IssueLink = new Link {name = name, type = "issue", url = name};
        }

        private Link IssueLink { get; }

        public override void UpdateTestResult(TestResult testCaseResult)
        {
            testCaseResult.links.Add(IssueLink);
        }
    }
}