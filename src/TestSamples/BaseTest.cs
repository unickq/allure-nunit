using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [AllureNUnit]
    [AllureParentSuite("AllTests")]
    class BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }
    }
}
