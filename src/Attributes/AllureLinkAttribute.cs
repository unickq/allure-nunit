using System;
using Allure.Commons;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AllureLinkAttribute : NUnitAttribute
    {
        public AllureLinkAttribute(string name, string url)
        {
            Link = new Link {name = name, type = "link", url = url};
        }

        public AllureLinkAttribute(string url)
        {
            Link = new Link {name = url, type = "link", url = url};
        }

        internal Link Link { get; }
    }
}