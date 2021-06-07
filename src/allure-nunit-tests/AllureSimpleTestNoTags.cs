using NUnit.Allure.Core;
using NUnit.Framework;

namespace allure_nunit_tests
{
    [AllureNUnit]
    public class AllureSimpleTestNoTags
    {
        [Test]
        public void SimpleTest() { }
    }
}