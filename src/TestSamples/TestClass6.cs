using System;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]
    internal class TestClass6 : BaseTest
    {
        [SetUp]
        public void NormalSetUp()
        {
            Console.WriteLine("I'm good setup");
        }

        [SetUp]
        public void BrokenSetup()
        {
            throw new Exception("SetUp exception");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("I will survive");
        }

        [Test]
        public void TestWontRun()
        {
            Console.WriteLine("Oh no!");
        }
    }
}