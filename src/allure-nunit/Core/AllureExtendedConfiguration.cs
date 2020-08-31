using System.Collections.Generic;
using Allure.Commons.Configuration;

namespace NUnit.Allure.Core
{
    internal class AllureExtendedConfiguration : AllureConfiguration
    {
        public HashSet<string> BrokenTestData { get; set; } = new HashSet<string>();

        protected AllureExtendedConfiguration(string title, string directory, HashSet<string> links) : base(title,
            directory, links)
        {
        }
    }
}