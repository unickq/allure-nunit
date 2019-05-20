using System;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]
    internal class TestClass7 : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("I'm an unwrapped SetUp");
        }

        [TearDown]
        public void TearDown()
        {
            AllureExtensions.WrapSetUpTearDownParams(() =>
            {
                Thread.Sleep(750);
                Console.WriteLine("Example of wrapped TearDown");
            }, "Custom TearDown name here");
        }


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("I'm an unwrapped OneTimeSetUp");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //OneTimeTearDown can't be wrapped at all
            Console.WriteLine("I'm an unwrapped OneTimeTearDown");
        }


        [Test]
        public void Test1()
        {
            Console.WriteLine("Test1");
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("Test2");
        }

        [Test, Combinatorial]
        [AllureLink("https://github.com/unickq/allure-nunit/issues/17")]
        public void TestExample(
            [Range(1, 3)] int valA,
            [Values(null, 2)] int? valB
        )
        {
            Assert.AreEqual(valA, valB);
        }

        [Test]
        [AllureIssue("18")]
        public void LocaleAttachments()
        {
            Console.WriteLine("Test attachment");
            Console.WriteLine("Проверка");
            Console.WriteLine("チェックする");
            Console.WriteLine("تفتيش");
        }
    }
}