using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.Bind<IProviderTrackingService>().To<ProviderTrackingService>();
            //MockKernel.GetMock<IRssReader>().Reset();
        }

        [Category("ProviderTrackingService")]
        [TestCase(1, TestName = "Should get all updated rss files from all providers")]
        public void GetUpdatedRssFilesFromNewsProvidersTest(int testcase)
        {
            //var expected = 0;

            var testNewsProviders = new List<NewsProvider>
            {
                new NewsProvider { Id = 1, Name = "Provider1hasRss", Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 6:35:55 PM") },
                new NewsProvider { Id = 2, Name = "Provider2hasRss",  Link = "rssLink", LastBuildDate = Convert.ToDateTime("5/23/2016 0:35:55 PM") },
                new NewsProvider { Id = 3, Name = "Provider3withoutRss",  Link = "noRssLink" }
            };

            //switch (testcase)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        testUserProviders.AddRange(new List<UserNewsProviderModels> { model1, model2 });
            //        expected = 2;
            //        break;
            //    case 2:
            //        testUserProviders.AddRange(new List<UserNewsProviderModels> { model1, model2, model3, model4 });
            //        expected = 4;
            //        break;
            //    case 3:
            //        testUserProviders.Add(model4);
            //        expected = 2;
            //        break;
            //}

            var testXmlFile = new XDocument(new XElement(
                "Root",
                new XElement("lastBuildDate", "Mon, 23 May 2016 13:33:08 +0300"),
                new XElement("Child2", "data2"),
                new XElement("Info3", "info3")));

            MockKernel.GetMock<INewsProviderService>().Setup(np => np.GetAll()).Returns(testNewsProviders);
            MockKernel.GetMock<IRssReader>().Setup(reader => reader.GetRssFileByLink("rssLink")).Returns(testXmlFile);
            int actual =
                MockKernel.Get<IProviderTrackingService>().GetUpdatedRssFilesFromNewsProviders().ToList().Count;

            Assert.AreEqual(1, actual);


        }
    }
}
