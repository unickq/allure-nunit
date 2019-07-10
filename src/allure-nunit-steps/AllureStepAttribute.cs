using System;
using AspectInjector.Broker;

namespace NUnit.Allure.Steps
{
    [Injection(typeof(AllureStepAspect))]
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureStepAttribute : Attribute
    {
        public AllureStepAttribute(string name = "")
        {
            StepName = name;
        }

        public string StepName { get; }
    }
}