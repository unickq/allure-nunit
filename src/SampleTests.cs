using System;
using System.Globalization;
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
        [AllureTest]
        [AllureTag("1", "2", "3")]
        [AllureTag("2")]
        [AllureTag("3")]
        [AllureSeverity(AllureSeverity.Critical)]
        [AllureIssue("Issue1", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
        public void Xxx1()
        {
            throw new NotFiniteNumberException("ASDSADASDSADSADAS");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(AllureSeverity.Critical)]
        [AllureIssue("Issue33", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
        public void Xxx2()
        {
            Assert.Ignore();
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(AllureSeverity.Critical)]
        [AllureIssue("Issue33", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
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
        [AllureSeverity(AllureSeverity.Critical)]
        [AllureIssue("Issue1", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
        public void Xxx1()
        {
            throw new NotFiniteNumberException("ASDSADASDSADSADAS");
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureSeverity(AllureSeverity.Blocker)]
        [AllureIssue("Issue33", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
        public void Xxx2()
        {
            Assert.Ignore();
        }

        [Test]
        [AllureTag("1")]
        [AllureTag("2")]
        [AllureIssue("Issue33", "http://ya.ru")]
        [AllureIssue("Issu2", "http://ya.ru")]
        [AllureSeverity(AllureSeverity.Minor)]
        [AllureFeature("FASDQWE")]
        public void Xxx3()
        {
            Assert.Pass();
        }
    }
}