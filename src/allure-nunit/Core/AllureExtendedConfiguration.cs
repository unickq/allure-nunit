using System.Collections.Generic;
using Allure.Commons.Configuration;
using Newtonsoft.Json;

namespace NUnit.Allure.Core
{
    internal class AllureExtendedConfiguration : AllureConfiguration
    {
        public HashSet<string> BrokenTestData { get; set; } = new HashSet<string>();

        [JsonConstructor]
        protected AllureExtendedConfiguration(string title, string directory, HashSet<string> links) : base(title,
            directory, links)
        {
        }
    }
}