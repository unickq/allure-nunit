using System;
using Allure.Commons;
using AspectInjector.Broker;

namespace NUnit.Allure.Steps
{
    [Aspect(Scope.Global)]
    [Injection(typeof(AllureStepAttribute))]
    [AttributeUsage(AttributeTargets.Method)]
    public class AllureStepAttribute : Attribute
    {
        [Advice(Kind.Around, Targets = Target.Method)]
        public object WrapStep(
            [Argument(Source.Name)] string name,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            var id = Guid.NewGuid().ToString();
            var stepResult = new StepResult {name = name};
            object result;
            try
            {
                AllureLifecycle.Instance.StartStep(id, stepResult);
                result = method(arguments);
                AllureLifecycle.Instance.StopStep(step => stepResult.status = Status.passed);
            }
            catch (Exception e)
            {
                AllureLifecycle.Instance.StopStep(step =>
                {
                    step.statusDetails = new StatusDetails
                    {
                        message = e.Message,
                        trace = e.StackTrace
                    };
                    step.status = Status.failed;
                });
                throw;
            }
            return result;
        }
    }
}
