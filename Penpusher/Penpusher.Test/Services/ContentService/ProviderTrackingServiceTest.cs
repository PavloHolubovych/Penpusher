using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Models;
using Penpusher.Services;
using Penpusher.Services.ContentService;

namespace Penpusher.Test.Services.ContentService
{
    [TestFixture]
    public class ProviderTrackingServiceTest : TestBase
    {
        private readonly List<NewsProvider> testNewsProviders = new List<NewsProvider>
            {
                new NewsProvider { Id = 1, Name = "Provider1hasRss", Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 6:35:55 PM") },
                new NewsProvider { Id = 2, Name = "Provider2hasRss",  Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 0:35:55 PM") },
                new NewsProvider { Id = 3, Name = "Provider3withoutRss",  Link = "noRssLink" }
            };

        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.Bind<IProviderTrackingService>().To<ProviderTrackingService>();
        }

        [Category("ProviderTrackingService")]
        [TestCase("Mon, 23 May 2016 12:33:08 +0300", 0, TestName = "Should not get rss files from providers if lastBuildBuild is not a later date")]
        [TestCase("Mon, 23 May 2016 13:33:08 +0300", 1, TestName = "Should get 1 updated rss file from all providers")]
        [TestCase("Mon, 23 May 2016 19:33:08 +0300", 2, TestName = "Should get all updated rss files from all providers")]
        [TestCase(null, 2, TestName = "Should get all updated rss files if lastBuildDate of channel is null or empty")]
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
        [TestCase(TestName = "Should return empty collection of updatedRssChannels if income scope of providers is empty")]
        public void GetUpdatedRssFilesFromNewsProvidersTest()
        {
            MockKernel.GetMock<INewsProviderService>().Setup(np => np.GetAll()).Returns(new List<NewsProvider>());
            MockKernel.GetMock<IRssReader>().Setup(reader => reader.GetRssFileByLink("rssLink")).Returns(new XDocument());
            int actual =
                MockKernel.Get<IProviderTrackingService>().GetUpdatedRssFilesFromNewsProviders().ToList().Count;

            Assert.AreEqual(0, actual);
        }

        [Category("ProviderTrackingService")]
        [TestCase(TestName = "Should get collection of Channels and invoke method InsertNewArticles with right parametres")]
        public void UpdateArticlesFromNewsProvidersTest()
        {
            var testNewsProviders2 = new List<NewsProvider>
            {
                new NewsProvider { Id = 5, Name = "Provider1hasRss", Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 6:35:55 PM") }
            };

            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2", IdNewsProvider = 5 },
                new Article { Id = 4, Title = "article2", IdNewsProvider = 5 }
            };

            var testXmlFile = new XDocument(new XElement(
                "root",
                    new XElement(
                    "channel",
                        new XElement("lastBuildDate", "Mon, 23 May 2016 19:35:55 +0300"),
                        new XElement("Info3", "info3"))));

            var rssChannelModel = new RssChannelModel { ProviderId = 5, LastBuildDate = Convert.ToDateTime("5/23/2016 7:35:55 PM"), RssFile = testXmlFile };
            var channelsCollection = new List<RssChannelModel> { rssChannelModel };

            MockKernel.GetMock<INewsProviderService>().Setup(np => np.GetAll()).Returns(testNewsProviders2);
            MockKernel.GetMock<IRssReader>().Setup(reader => reader.GetRssFileByLink("rssLink")).Returns(testXmlFile);
            MockKernel.GetMock<IParser>().Setup(p => p.GetParsedArticles(null)).Returns(testArticles);

            MockKernel.Get<IProviderTrackingService>().UpdateArticlesFromNewsProviders();

            MockKernel.GetMock<IDataBaseServiceExtension>()
                .Verify(
                    d => d.InsertNewArticles(
                        It.Is<IEnumerable<RssChannelModel>>(
                            actualCollection => IsListsEqual(
                                actualCollection.ToList(), channelsCollection.ToList()))),
                    Times.Once);
        }

        [Category("ProviderTrackingService")]
        public void DependencyInjectionTestForIProviderTrackingService()
        {
            IKernel kernel = NinjectWebCommon.Kernel;
            var service = kernel.Get<IProviderTrackingService>();
            Assert.NotNull(service);
            service.UpdateArticlesFromNewsProviders();
        }

        private static bool IsListsEqual(List<RssChannelModel> actualCollection, List<RssChannelModel> expectedCollection)
        {
            if (actualCollection.Count != expectedCollection.Count)
            {
                return false;
            }

            for (var i = 0; i < actualCollection.Count(); i++)
            {
                if (actualCollection[i].LastBuildDate != expectedCollection[i].LastBuildDate ||
                    actualCollection[i].ProviderId != expectedCollection[i].ProviderId ||
                    actualCollection[i].RssFile != expectedCollection[i].RssFile)
                {
                    return false;
                }
            }
            return true;
        }
    }
}