using System;
using NUnit.Framework;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureOwnerAttribute : NUnitAttribute
    {
        public AllureOwnerAttribute(string owner)
        {
            Owner = owner;
        }

        internal string Owner { get; }
    }
}