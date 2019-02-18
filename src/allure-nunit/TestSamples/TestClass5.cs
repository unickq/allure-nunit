using System;
using System.Collections.Generic;
using System.Threading;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture, Description("Steps usage example")]
    internal class TestClass5 : BaseTest
    {
        private readonly string[] _adeleSong =
        {
            "Hello, it's me",
            "I was wondering if after all these years you'd like to meet",
            "To go over everything",
            "They say that time's supposed to heal ya",
            "But I ain't done much healing"
        };

        [Test]
        [Category("Action")]
        [AllureEpic("Steps")]
        [AllureSuite("Action")]
        [AllureSubSuite("Adele Song")]
        public void TestWithStepsAction()
        {
            foreach (var str in _adeleSong)
                AllureLifecycle.Instance.WrapInStep(() =>
                {
                    Thread.Sleep(100 * new Random().Next(1, 10));
                    Console.WriteLine(str);
                }, $"Printing {str}");
        }


        [Test]
        [Category("Func")]
        [AllureEpic("Steps")]
        [AllureSuite("Func")]
        [AllureSubSuite("Adele Song")]
        public void TestWithStepsFunc()
        {
            foreach (var str in _adeleSong)
            {
                var count = AllureLifecycle.Instance.WrapInStep(() =>
                {
                    Thread.Sleep(100 * new Random().Next(1, 10));
                    return str.Length;
                }, $"Calculating length of {str}");
                Console.WriteLine($"{str} - {count} chars");
            }
        }
    }
}