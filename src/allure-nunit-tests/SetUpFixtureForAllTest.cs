using Allure.Commons;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace allure_nunit_tests
{
    [SetUpFixture]
    public class SetUpFixtureForAllTest
    {
        [OneTimeSetUp]
        public static void CleanupResultDirectory()
        {
            //AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
            //    "Clear Allure Results Directory");
        }
    }
}