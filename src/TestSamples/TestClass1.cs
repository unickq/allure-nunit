using System;
using System.Threading.Tasks;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    class TestClass1 : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Hello, this is TestClass1 setUp");
        }

        [Test(Description = "XXX")]
        [AllureTag("TC-1")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-1")]
        [AllureOwner("unickq")]
        [AllureSuite("PassedSuite")]
        [AllureSubSuite("NoAssert")]
        [AllureSubSuite("Simple")]      
        public void SimpleTestPassed()
        {
            Task.Run(() => Console.WriteLine("QQQQ"));
            Task.Run(() => Console.WriteLine("AAAA"));
            Task.Run(() => Console.WriteLine("OOOOOOO"));
            Console.WriteLine("Passed");
            Console.WriteLine(DateTime.Now);
        }

        [Test]
        [AllureSuite("IgnoredSuite")]
        [AllureSubSuite("NoAssert")]
        [Ignore("I'm just an ignored test")]
        public void SimpleTestIgnored()
        {
            Console.WriteLine("Ignored");
        }

        [Test(Author = "unickq")]
        [Description("OLOLO")]
        [AllureSuite("FailedSuite")]
        [AllureSubSuite("DoesNotThrow")]
        [AllureLink("Google", "https://google.com")]
        public void SimpleTestFailed()
        {
            Console.WriteLine("Failed");
            Assert.DoesNotThrow(() =>
            {
                throw new Exception("I'm an exception");
            });
        }
    }
}