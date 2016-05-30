using System;
using System.IO;
using System.Xml.Linq;
using Ninject;
using NUnit.Framework;
using Penpusher.Services.ContentService;

namespace Penpusher.Test.Services.ContentService
{
    [TestFixture]
    public class RssReaderTest : TestBase
    {
        private readonly string testLink = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "TestFiles\\"),
            "testRssFile.xml");
        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IRssReader>().To<RssReader>();
        }

        [Category("RssReader")]
        [TestCase("Should return file.xml after loading a link")]
        public void GetRssFileByLinkTest(string link)
        {
            var mockReader = MockKernel.Get<IRssReader>();
            XDocument testResult = mockReader.GetRssFileByLink(testLink);
            Assert.IsNotNull(testResult);
        }
    }
}