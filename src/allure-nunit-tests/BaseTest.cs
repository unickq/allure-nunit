using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace allure_nunit_tests
{
    [AllureNUnit]
    [AllureParentSuite("Root Suite")]
    public class BaseTest
    {
        [OneTimeSetUp]
        public void CleanupResultDirectory()
        {
            AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
                "Clear Allure Results Directory");
        }
    }
}