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
        [Test]
        [AllureTag("1", "2", "3")]
        [AllureTag("2")]
        [AllureTag("3")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("t1")]
        [AllureIssue("t2")]
        public void Xxx1()
        {
            throw new NotFiniteNumberException("ASDSADASDSADSADAS");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("t1")]
        [AllureIssue("t2")]
        public void Xxx2()
        {
            Assert.Ignore("I'm just ignored :(");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureIssue("t1")]
        [AllureIssue("t2")]
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
        [AllureIssue("JIRA-123")]
        [AllureIssue("JIRA-12")]
        public void Xxx2()
        {
            Console.WriteLine(AllureLifecycle.Instance.ResultsDirectory);
            Assert.Ignore();
        }

        [Test(Description = "Test example description")]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureIssue("t1")]
        [AllureIssue("t2")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureFeature("FASDQWE")]
        [Retry(100)]
        public void Xxx3()
        {
            var z = DateTime.Now.Millisecond;
            if (z > 50) Assert.Fail(z.ToString());
        }
    }
}