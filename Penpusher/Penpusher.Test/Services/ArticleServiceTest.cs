using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Test.Services
{
    [TestFixture]
    public class ArticleServiceTest : TestBase
    {
        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.GetMock<IRepository<Article>>().Reset();
        }

        [Category("ArticleService")]
        [TestCase("my title", true, TestName = "CheckDoesExists title exists")]
        [TestCase("my title123321", false, TestName = "CheckDoesExists title is missing ")]
        public void CheckDoesExistsTest(string title, bool expected)
        {
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "test title" },
                new Article { Id = 2, Title = "my title" },
                new Article { Id = 3, Title = "my title" }
            };
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll()).Returns(testArticles);
            bool actual = MockKernel.Get<IArticleService>().CheckDoesExists(title);
            Assert.AreEqual(actual, expected);
        }

        [Category("ArticleService")]
        [TestCase(TestName = "Checks if ArticleService adds a new article")]
        public void AddArticleTest()
        {
            var testArticle = new Article { Id = 1, Title = "newArticle" };
            MockKernel.GetMock<IRepository<Article>>().Setup(ad => ad.Add(It.Is<Article>(a => a.Id == 1))).Returns(testArticle);

            Article actual = MockKernel.Get<IArticleService>().AddArticle(testArticle);
            MockKernel.GetMock<IRepository<Article>>().Verify(r => r.Add(It.Is<Article>(a => a.Id == 1)), Times.Once);

            Assert.AreEqual(testArticle, actual, "Another article");
        }

        [Category("ArticleService")]
        [TestCase("article1", 1, TestName = "Should find article by title")]
        [TestCase("article 1", 1, TestName = "Should find article by title contains space")]
        [TestCase("article2", 1, TestName = "Should find first article")]
        [TestCase("aticle3", 0, TestName = "Not exist article")]
        [TestCase(default(string), 0, TestName = "Not title is null")]
        public void FindArticleTest(string title, int expectedCount)
        {
            // arrange
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2" },
                new Article { Id = 2, Title = "article 1" },
                new Article { Id = 3, Title = "article1" },
            };

            MockKernel.GetMock<IRepository<Article>>().Setup(rm => rm.GetAll()).Returns(testArticles);

            // act
            IEnumerable<Article> result = MockKernel.Get<ArticleService>().Find(title);

            // assert
            int expected = result.Count();
            Assert.AreEqual(expected, expectedCount);
        }

        [Category("ArticleService")]
        [TestCase(1, true, TestName = "Should find articles by providerId = 1")]
        [TestCase(4, false, TestName = "Shouldn't find any articles by providerId = 4")]
        public void GetArticlesFromProviderTest(int providerId, bool expected)
        {
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2", IdNewsProvider = 1 },
                new Article { Id = 2, Title = "article1", IdNewsProvider = 2 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 3 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 0 }
            };
            MockKernel.GetMock<IRepository<Article>>().Setup(_ => _.GetAll()).Returns(testArticles);
            bool result = MockKernel.Get<ArticleService>().GetArticlesFromProvider(providerId).Any();
            Assert.AreEqual(expected, result);
        }

        [Category("ArticleService")]
        [TestCase(1, 0, TestName = "Should find 0 articles if there is no any provider in scope")]
        [TestCase(2, 4, TestName = "Should find all articles if user is subscribed to several providers")]
        [TestCase(3, 0, TestName = "Should find 0 articles if user has no subscriptions for providers in scope")]
        public void GetArticlesFromSelectedProvidersTest(int testCase, int expected)
        {
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2", IdNewsProvider = 1 },
                new Article { Id = 2, Title = "article1", IdNewsProvider = 2 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 4 },
                new Article { Id = 4, Title = "article2", IdNewsProvider = 4 }
            };

            var testUserProviders = new List<UserNewsProviderModels>();
            var model1 = new UserNewsProviderModels { Name = "provider1", IdNewsProvider = 1 };
            var model2 = new UserNewsProviderModels { Name = "provider2", IdNewsProvider = 2 };
            var model3 = new UserNewsProviderModels { Name = "provider3", IdNewsProvider = 3 };
            var model4 = new UserNewsProviderModels { Name = "provider3", IdNewsProvider = 4 };

            if (testCase == 2)
            {
                testUserProviders.AddRange(new List<UserNewsProviderModels> { model1, model2, model4 });
            }

            if (testCase == 3)
            {
                testUserProviders.Add(model3);
            }

            MockKernel.GetMock<IRepository<Article>>().Setup(_ => _.GetAll()).Returns(testArticles);
            IEnumerable<Article> result = MockKernel.Get<ArticleService>().GetArticlesFromSelectedProviders(testUserProviders);

            Article[] enumerable = result as Article[] ?? result.ToArray();
            Assert.AreEqual(expected, enumerable.Length);
        }
    }
}