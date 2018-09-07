using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

namespace NUnit.Allure.TestSamples
{
    [TestFixture]
    internal class TestClass3 : BaseTest
    {
        [TearDown]
        public void AddAttachmentAfter()
        {
            AllureLifecycle.Instance.AddAttachment(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "AllureConfig.json"),
                "AllureConfig.json");
        }

        [Test(Description = "Attachments tests")]
        [AllureTag("TC-1")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("unickq")]
        [AllureSuite("Attachment")]
        [AllureSubSuite("Simple")]
        public void AttachmentSimple()
        {
            Console.WriteLine("With Attachment");
            Console.WriteLine(DateTime.Now);
        }

        [Test(Description = "Attachments tests")]
        [AllureTag("TC-1")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("unickq")]
        [AllureSuite("Attachment")]
        [AllureSubSuite("Simple")]
        [AllureLink("Github", "https://github.com")]
        [AllureLink("https://google.com")]
        [AllureIssue("ISSUE-123")]
        [AllureTms("TMS-123")]
        public void AttachmentTwo()
        {
            Console.WriteLine("With Attachment 2");
            Console.WriteLine(DateTime.Now);
            AddAttachmentAfter();
        }
    }
}