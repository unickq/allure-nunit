using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace allure_nunit_tests
{
    [AllureNUnit]
    [AllureParentSuite("Root Suite")]
    [AllureEpic("Root Epic")]
    public class BaseTest
    {
        // Used to inherit attributes
    }
}