using System;
using System.Linq;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllureFixtureAttribute : BaseAllureAttribute
    {
        private string Value { get; }

        public AllureFixtureAttribute(string description = "")
        {
            Value = description;
        }

        public override void BeforeTest(ITest test)
        {
            var fixture = new TestResultContainer
            {
                uuid = test.Id,
                name = test.ClassName
            };
            Allure.StartTestContainer(fixture);
        }

        public override void AfterTest(ITest test)
        {
            if (test.HasChildren)
                Allure.UpdateTestContainer(test.Id, t => t.children.AddRange(test.Tests.Select(s => s.Id)));
            if (!string.IsNullOrEmpty(Value)) Allure.UpdateTestContainer(test.Id, t => t.description = Value);
            Allure.StopTestContainer(test.Id);
            Allure.WriteTestContainer(test.Id);
        }


        public override ActionTargets Targets => ActionTargets.Suite;
    }
}