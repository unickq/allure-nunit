using System;
using Allure.Commons;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureTmsAttribute : NUnitAttribute
    {
        public AllureTmsAttribute(string name, string url)
        {
            TmsLink = new Link { name = name, type = "tms", url = url };
        }

        public AllureTmsAttribute(string name)
        {
            TmsLink = new Link { name = name, type = "tms", url = name };
        }

        internal Link TmsLink { get; }
    }
}