﻿using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureParentSuiteAttribute : AllureTestCaseAttribute
    {
        public AllureParentSuiteAttribute(string parentSuite)
        {
            ParentSuite = parentSuite;
        }

        private string ParentSuite { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            testResult.labels.Add(Label.ParentSuite(ParentSuite));
        }
    }
}