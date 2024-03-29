﻿using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureSubSuiteAttribute : AllureTestCaseAttribute
    {
        public AllureSubSuiteAttribute(string subSuite)
        {
            SubSuite = subSuite;
        }

        private string SubSuite { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            testResult.labels.Add(Label.SubSuite(SubSuite));
        }
    }
}