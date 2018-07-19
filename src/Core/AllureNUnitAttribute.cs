using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.Allure.Core
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AllureNUnitAttribute : NUnitAttribute, ITestAction
    {
        private AllureNUnitHelper _allureNUnitHelper;
        private readonly bool _isWrapedIntoStep;

        public AllureNUnitAttribute(bool wrapIntoStep = true)
        {
            _isWrapedIntoStep = wrapIntoStep;
        }

        public void BeforeTest(ITest test)
        {
            _allureNUnitHelper = new AllureNUnitHelper(test);
            _allureNUnitHelper.StartTestContainer();
            _allureNUnitHelper.StartTestCase();
            _allureNUnitHelper.StartTestStep(_isWrapedIntoStep);
        }

        private Object thisLock = new Object();

        public void AfterTest(ITest test)
        {
            _allureNUnitHelper.StopAll(_isWrapedIntoStep);
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}