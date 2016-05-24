using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ninject;
using NUnit.Framework;
using Penpusher.Services;
using Penpusher.Services.ContentService;

namespace Penpusher.Test.Services.ContentService
{
    [TestFixture]
    public class ProviderTrackingServiceTest : TestBase
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.Bind<IProviderTrackingService>().To<ProviderTrackingService>();
        }

        private readonly List<NewsProvider> testNewsProviders = new List<NewsProvider>
            {
                new NewsProvider { Id = 1, Name = "Provider1hasRss", Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 6:35:55 PM") },
                new NewsProvider { Id = 2, Name = "Provider2hasRss",  Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 0:35:55 PM") },
                new NewsProvider { Id = 3, Name = "Provider3withoutRss",  Link = "noRssLink" }
            };

        [Category("ProviderTrackingService")]
        [TestCase("Mon, 23 May 2016 12:33:08 +0300", 0, TestName = "Should not get rss files from providers")]
        [TestCase("Mon, 23 May 2016 13:33:08 +0300", 1, TestName = "Should get 1 updated rss file from all providers")]
        [TestCase("Mon, 23 May 2016 19:33:08 +0300", 2, TestName = "Should get all updated rss files from all providers")]
        [TestCase(null, 2, TestName = "Should get all updated rss files lastBuildDate of channel is null or empty")]
        public void GetUpdatedRssFilesFromNewsProvidersTest(string lastBuildDate, int expected)
        {
            var testXmlFile = new XDocument(new XElement(
                "root",
                    new XElement(
                    "channel",
                        new XElement("lastBuildDate", lastBuildDate),
                        new XElement("Child2", "data2"),
                        new XElement("Info3", "info3"))));

            MockKernel.GetMock<INewsProviderService>().Setup(np => np.GetAll()).Returns(testNewsProviders);
            MockKernel.GetMock<IRssReader>().Setup(reader => reader.GetRssFileByLink("rssLink")).Returns(testXmlFile);
            int actual =
                MockKernel.Get<IProviderTrackingService>().GetUpdatedRssFilesFromNewsProviders().ToList().Count;

            Assert.AreEqual(expected, actual);
        }

        [Category("ProviderTrackingService")]
        [TestCase(2, TestName = "Should get all rss files which does not contain tag lastBuildDate")]
        public void GetUpdatedRssFilesFromNewsProvidersTest(int expected)
        {
            var testXmlFile = new XDocument(new XElement(
                "root",
                    new XElement(
                    "channel",
                        new XElement("Child2", "data2"),
                        new XElement("Info3", "info3"))));

            MockKernel.GetMock<INewsProviderService>().Setup(np => np.GetAll()).Returns(testNewsProviders);
            MockKernel.GetMock<IRssReader>().Setup(reader => reader.GetRssFileByLink("rssLink")).Returns(testXmlFile);
            int actual =
                MockKernel.Get<IProviderTrackingService>().GetUpdatedRssFilesFromNewsProviders().ToList().Count;

            Assert.AreEqual(expected, actual);
        }


        [Category("ProviderTrackingService")]
        public void DependencyInjectionTestForIProviderTrackingService()
        {
            IKernel kernel = NinjectWebCommon.Kernel;
            var service = kernel.Get<IProviderTrackingService>();
            Assert.NotNull(service);
            service.UpdateArticlesFromNewsProviders();
        }
    }
}