using System;
using System.Collections;
using System.Threading;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]
    class TestClass4 : BaseTest
    {
        [Test]
        [AllureTag("NUnit", "Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureFeature("Core")]
        [AllureSuite("RetrySuite")]
        [AllureSubSuite("Range")]
        [AllureParentSuite("With parameters")]
        public void EvenTest([Range(0, 5)] int value)
        {
            Console.WriteLine("Hi there");
            Assert.IsTrue(value % 2 == 0, $"Oh no :( {value} % 2 = {value % 2}");
        }

        [AllureSuite("TestCaseSource")]
        [AllureSubSuite("Returns")]
        [AllureParentSuite("With parameters")]
        [Test, TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesReturns))]
        public int DivideTestReturns(int n, int d)
        {
            Console.WriteLine($"Validating {n} / {d} ");
            return n / d;
        }

        [AllureSuite("TestCaseSource")]
        [AllureSubSuite("Simple")]
        [AllureParentSuite("With parameters")]
        [Test, TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCases))]
        public void DivideTest(int n, int d, int r)
        {
            Thread.Sleep(100 * new Random().Next(1, 25));
            Console.WriteLine($"Validating {n} / {d} = {r}");
            Assert.AreEqual(r, n / d);
        }
    }

    public class TestDataClass
    {
        public static IEnumerable TestCasesReturns
        {
            get
            {
                yield return new TestCaseData(12, 3).Returns(4);
                yield return new TestCaseData(12, 2).Returns(6);
                yield return new TestCaseData(12, 4).Returns(3);
            }
        }
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(12, 3, 4);
                yield return new TestCaseData(12, 2, 6);
                yield return new TestCaseData(12, 4, 3);
            }
        }
    }
}
