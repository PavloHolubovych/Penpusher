using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Models;
using Penpusher.Services;

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
            MockKernel.GetMock<IRepository<NewsProvider>>().Reset();
        }

        [Category("ProviderTrackingService")]
        [TestCase(0, TestName = "Should get all updated rss files from all providers")]
        public void GetUpdatedRssFilesFromNewsProvidersTest()
        {
        }
    }
}
