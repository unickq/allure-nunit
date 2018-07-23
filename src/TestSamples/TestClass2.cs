using System;
using System.Globalization;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]

    class TestClass2 : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Hello, this is TestClass1 setUp");
        }

        [Test]
        [AllureTag("TC-1")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("ISSUE-1")]
        [AllureSuite("RetrySuite")]
        [AllureSubSuite("Range")]
        [AllureOwner("unickq")]
        [AllureParentSuite("With parameters")]
        public void RangeTest([Range(0.2, 0.6, 0.2)] double d)
        {
            Assert.Pass(d.ToString(CultureInfo.InvariantCulture));
        }

        private int _retryInt;

        [Test]
        [Retry(5)]
        [AllureEpic("Retry")]
        [AllureFeature("RetrySmall")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureSuite("RetrySuite")]
        [AllureParentSuite("With parameters")]
        [AllureOwner("Nick")]
        public void RetryTest()
        {
            Assert.That(++_retryInt, Is.GreaterThan(3));
        }

        [Test(Author = "Nick", Description = "Retry example")]
        [Category("Retry")]
        [Retry(100)]
        [AllureEpic("Retry")]
        [AllureFeature("RetryBig")]
        [AllureSuite("RetrySuite")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureTag("tag1", "tag2", "tag3")]
        public void RetryManyTest()
        {
            Assert.That(DateTime.Now.Millisecond, Is.LessThan(50));
        }

        [Test, Pairwise]
        public void PairWiseTest([Values("a", "b", "c")] string str)
        {
            Console.WriteLine(str);
        }


        [Test, Repeat(5)]
        public void RepeatTest()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}