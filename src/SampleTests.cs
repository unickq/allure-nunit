using System;
using System.Globalization;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

namespace NUnit.Allure
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    [AllureFixture("Just fixture example")]
    public class Class1
    {
        public const string Url = "https://github.com/unickq/allure-nunit/issues?utf8=✓&q=";

        [Test]
        [AllureTag("1", "2", "3")]
        [AllureTag("2")]
        [AllureTag("3")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("Issue1", Url)]
        [AllureIssue("Issu2", Url)]
        public void Xxx1()
        {
            throw new NotFiniteNumberException("ASDSADASDSADSADAS");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("Issue33", Url)]
        [AllureIssue("Issu2", Url)]
        public void Xxx2()
        {
            Assert.Ignore("I'm just ignored :(");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("Issue33", Url)]
        [AllureTms("Issu2", Url)]
        [AllureFeature("FASDQWE")]
        public void Xxx3([Range(0.2, 0.6, 0.2)] double d)
        {
            Assert.Pass(d.ToString(CultureInfo.InvariantCulture));
        }
    }

    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    [AllureFixture("Just fixture example2")]
    public class Class2
    {
        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureTag("3")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureIssue("AI1", Class1.Url)]
        [AllureIssue("AI2", Class1.Url)]
        [Retry(5)]
        public void Xxx1()
        {
            Console.WriteLine("!");
            Assert.DoesNotThrow(() => throw new NotFiniteNumberException("I'm a exception"));
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureIssue("q2", Class1.Url)]
        [AllureIssue("q1", Class1.Url)]
        public void Xxx2()
        {
            Assert.Ignore();
        }

        [Test(Description = "Test example description")]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureIssue("t1", Class1.Url)]
        [AllureIssue("t2", Class1.Url)]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureFeature("FASDQWE")]
        [Retry(5)]
        public void Xxx3()
        {
            Console.WriteLine("WooHoo");
            Assert.Fail();
        }
    }
}