// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Sigma" file="ArticleServiceTest.cs">
//   
// </copyright>
// <summary>
//   The article service test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
 
namespace Penpusher.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Ninject;
    using NUnit.Framework;
    using Penpusher.Services;
    using Models;

    /// <summary>
    /// The article service test class.
    /// </summary>
    [TestFixture]
    public class ArticleServiceTest : TestBase
    {
        /// <summary>
        /// The initialize.
        /// </summary>
        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.GetMock<IRepository<Article>>().Reset();
        }

        /// <summary>
        /// The check does exists test.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="expected"></param>
        [Category("ArticleService")]
        [TestCase("my title", true, TestName = "CheckDoesExists title exists")]
        [TestCase("my title123321", false, TestName = "CheckDoesExists title is missing ")]
        public void CheckDoesExistsTest(string title, bool expected)
        {
            var testArticles = new List<Article>()
            {
                new Article() { Id = 1, Title = "test title" },
                new Article() { Id = 2, Title = "my title" },
                new Article() { Id = 3, Title = "my title" }
            };
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll()).Returns(testArticles);
            bool actual = MockKernel.Get<IArticleService>().CheckDoesExists(title);
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// The add article test.
        /// </summary>
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

        /// <summary>
        /// The find article test.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="expectedCount">
        /// The expected Count.
        /// </param>
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

        /// <summary>
        /// Get articles from provider test.
        /// </summary>
        /// <param name="providerId">
        /// The provider id.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        [Category("ArticleService")]
        [TestCase(1, 1, TestName = "Should find articles by providerId = 1")]
        [TestCase(4, 0, TestName = "Shouldn't find any articles by providerId = 4")]
        public void GetArticlesFromProviderTest(int providerId, int expected)
        {
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2", IdNewsProvider = 1},
                new Article { Id = 2, Title = "article 1", IdNewsProvider = 2 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 3 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 0 },
            };
            MockKernel.GetMock<IRepository<Article>>().Setup(_ => _.GetAll()).Returns(testArticles);
            IEnumerable<Article> result = MockKernel.Get<ArticleService>().GetArticlesFromProvider(providerId);
            Article[] enumerable = result as Article[] ?? result.ToArray();
            if (providerId == 4) { Assert.IsEmpty(enumerable); } else { Assert.IsNotEmpty(enumerable); }
        }

        [Category("ArticleService")]
        [TestCase(0, TestName = "Should find all articles by selection of 0 providers")]
        [TestCase(1, TestName = "Should find all articles by selection of 2 providers")]
        [TestCase(2, TestName = "Should find all articles by selection of 4 providers")]
        [TestCase(3, TestName = "Should find all articles by selection of 1 provider")]
        [TestCase(4, TestName = "Should find 0 articles")]
        public void GetArticlesFromSelectedProvidersTest(int testcase)
        {
            var expected = 0;

            var testUserProviders = new List<UserNewsProviderModels>();
            var model1 = new UserNewsProviderModels { Name = "provider1", IdNewsProvider = 1 };
            var model2 = new UserNewsProviderModels { Name = "provider2", IdNewsProvider = 2 };
            var model3 = new UserNewsProviderModels { Name = "provider3", IdNewsProvider = 3 };
            var model4 = new UserNewsProviderModels { Name = "provider3", IdNewsProvider = 4 };

            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2", IdNewsProvider = 1 },
                new Article { Id = 2, Title = "article1", IdNewsProvider = 2 },
                new Article { Id = 3, Title = "article1", IdNewsProvider = 4 },
                new Article { Id = 4, Title = "article2", IdNewsProvider = 4 }
            };

            switch (testcase)
            {
                case 0:
                    break;
                case 1:
                    testUserProviders.AddRange(new List<UserNewsProviderModels> { model1, model2 });
                    expected = 2;
                    break;
                case 2:
                    testUserProviders.AddRange(new List<UserNewsProviderModels> { model1, model2, model3, model4 });
                    expected = 4;
                    break;
                case 3:
                    testUserProviders.Add(model4);
                    expected = 2;
                    break;
                case 4:
                    testUserProviders.Add(model3);
                    break;
            }

            MockKernel.GetMock<IRepository<Article>>().Setup(_ => _.GetAll()).Returns(testArticles);
            IEnumerable<Article> result = MockKernel.Get<ArticleService>().GetArticlesFromSelectedProviders(testUserProviders);

            Article[] enumerable = result as Article[] ?? result.ToArray();
            Assert.AreEqual(expected, enumerable.Length);
        }
    }
}
