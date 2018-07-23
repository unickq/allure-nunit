using System;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]
    class TestClass5 : BaseTest
    {
        [Test]
        [AllureEpic("Song")]
        [AllureSuite("Adele")]
        [AllureSubSuite("Hello")]
        public void TestWithSteps()
        {
            foreach (var str in _adeleSong)
            {
                AllureNUnitHelper.WrapInStep(() =>
                {
                    Thread.Sleep(100 * new Random().Next(1, 10));
                    Console.WriteLine(str + Environment.NewLine);
                }, str);
            }
           
        }

        private readonly string[] _adeleSong = {
            "Hello, it's me",
            "I was wondering if after all these years you'd like to meet",
            "To go over everything",
            "They say that time's supposed to heal ya",
            "But I ain't done much healing"
        };
    }
}