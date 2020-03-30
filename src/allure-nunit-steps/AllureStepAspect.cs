using System;
using System.Reflection;
using Allure.Commons;
using AspectInjector.Broker;

namespace NUnit.Allure.Steps
{
    [Aspect(Scope.Global)]
    public class AllureStepAspect
    {
        [Advice(Kind.Around, Targets = Target.Method)]
        public object WrapStep(
            [Argument(Source.Name)] string name,
            [Argument(Source.Metadata)] MethodBase methodBase,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            var stepName = methodBase.GetCustomAttribute<AllureStepAttribute>().StepName;

            for (var i = 0; i < arguments.Length; i++)
            {
                stepName = stepName?.Replace("{" + i + "}", arguments[i].ToString());
            }

            var stepResult = string.IsNullOrEmpty(stepName)
                ? new StepResult {name = name, parameters = ParameterHelper.CreateParameters(arguments)}
                : new StepResult {name = stepName, parameters = ParameterHelper.CreateParameters(arguments)};

            object result;
            try
            {
                AllureLifecycle.Instance.StartStep(Guid.NewGuid().ToString(), stepResult);
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