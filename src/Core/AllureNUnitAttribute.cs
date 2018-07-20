using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Core
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AllureNUnitAttribute : NUnitAttribute, ITestAction
    {
        private readonly ThreadLocal<AllureNUnitHelper> _allureNUnitHelper = new ThreadLocal<AllureNUnitHelper>(true);
        private readonly bool _isWrapedIntoStep;

        public AllureNUnitAttribute(bool wrapIntoStep = true)
        {
            _isWrapedIntoStep = wrapIntoStep;
        }

        public void BeforeTest(ITest test)
        {
            _allureNUnitHelper.Value = new AllureNUnitHelper(test);
            _allureNUnitHelper.Value.StartAll(_isWrapedIntoStep);
        }

        public void AfterTest(ITest test)
        {
            _allureNUnitHelper.Value.StopAll(_isWrapedIntoStep);
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}