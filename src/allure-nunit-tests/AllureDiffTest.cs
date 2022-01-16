using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnit.Allure.Core;

namespace allure_nunit_tests
{
    [AllureSuite("Tests - ScreenDiff")]
    public class AllureDiffTest : BaseTest
    {
        [Test]
        [AllureName("Simple test with diff")]
        public void DiffTest()
        {
            AddDiffs();
        }

        [Test]
        [AllureName("Diff test with step")]
        public void DiffStepsTest()
        {
            AddDiffs();
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                AllureLifecycle.Instance.WrapInStep(() => { AddDiffs(); }, "Step Inside");
                AddDiffs();
            }, "StepOutSide");
        }

        public static void AddDiffs()
        {
            AllureLifecycle.Instance.AddScreenDiff("logo.png", "logo.png", "logo.png");
        }
    }
}