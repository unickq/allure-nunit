using System;
using System.Threading;
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
    }
}