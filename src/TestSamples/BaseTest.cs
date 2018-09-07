using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [AllureNUnit]
    [AllureParentSuite("AllTests")]
    internal class BaseTest
    {
        [OneTimeSetUp]
        public void CleanupResultDirectory()
        {
            AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
                "Cleanup Allure Results Directory");
        }
    }
}