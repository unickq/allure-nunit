﻿using System;
using Allure.Commons;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureTagAttribute : AllureTestCaseAttribute
    {
        public AllureTagAttribute(params string[] tags)
        {
            Tags = tags;
        }

        private string[] Tags { get; }

        public override void UpdateTestResult(TestResult testResult)
        {
            foreach (var tag in Tags)
                testResult.labels.Add(Label.Tag(tag));
        }
    }
}