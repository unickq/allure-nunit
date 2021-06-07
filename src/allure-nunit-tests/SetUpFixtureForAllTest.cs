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
            // not clearing the results as I am copying in some json files first
            //AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
            //    "Clear Allure Results Directory");
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            // maybe call out to the allure generate command here
        }
    }
}