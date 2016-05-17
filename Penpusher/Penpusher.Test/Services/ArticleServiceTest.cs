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
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.GetMock<IRepository<Article>>().Reset();
        }

        /// <summary>
        /// The check does exists test.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [Category("ArticleService")]
        [TestCase("my title", ExpectedResult = true, TestName = "CheckDoesExists title exists")]
        [TestCase("my title123321", ExpectedResult = false, TestName = "CheckDoesExists title is missing ")]
        public bool CheckDoesExistsTest(string title)
        {
            var testArticles = new List<Article>()
            {
                new Article() { Id = 1, Title = "test title" },
                new Article() { Id = 2, Title = "my title" },
                new Article() { Id = 3, Title = "my title" }
            };
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll()).Returns(testArticles);
            bool actual = MockKernel.Get<IArticleService>().CheckDoesExists(title);
            return actual;
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
            var result = MockKernel.Get<ArticleService>().Find(title);

            // assert
            var expected = result.Count();
            Assert.AreEqual(expected, expectedCount);
        }
    }
}
