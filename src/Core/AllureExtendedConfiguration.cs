using System.Collections.Generic;
using Allure.Commons.Configuration;

namespace NUnit.Allure.Core
{
    class AllureExtendedConfiguration : AllureConfiguration
    {
        public HashSet<string> BrokenTestData { get; set; } = new HashSet<string>();
    }
}
