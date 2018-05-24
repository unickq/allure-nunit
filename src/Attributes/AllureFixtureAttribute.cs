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
        public AllureFixtureAttribute(string description = "")
        {
            Description = description;
        }

        private string Description { get; }


        public override ActionTargets Targets => ActionTargets.Suite;

        public override void BeforeTest(ITest test)
        {
            var fixture = new TestResultContainer
            {
                uuid = test.Id,
                name = test.ClassName,
                descriptionHtml = Description
            };
            Allure.StartTestContainer(fixture);
        }

        public override void AfterTest(ITest test)
        {
            if (test.HasChildren)
                Allure.UpdateTestContainer(test.Id, t => t.children.AddRange(test.Tests.Select(s => s.Id)));
            Allure.StopTestContainer(test.Id);
            Allure.WriteTestContainer(test.Id);
        }
    }
}