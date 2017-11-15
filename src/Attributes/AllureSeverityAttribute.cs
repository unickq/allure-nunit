using System;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureSeverityAttribute : BaseAllureAttribute
    {
        private SeverityLevel Severity { get; }

        public AllureSeverityAttribute(SeverityLevel severity = SeverityLevel.normal)
        {
            Severity = severity;
        }


        public override void AfterTest(ITest test)
        {
            Allure.UpdateTestCase(x => x.labels.Add(Label.Severity(Severity)));

            base.AfterTest(test);
        }

        public override ActionTargets Targets => ActionTargets.Test;
    }
}