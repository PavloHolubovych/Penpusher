namespace Penpusher.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
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
        /// Test articles.
        /// </summary>
        private readonly List<Article> testData = new List<Article>()
        {
                new Article()

                {
                    Date = DateTime.Now,
                    Description = "Some description",
                    Id = 1,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "test title"
                },
                new Article()
                {
                    Date = DateTime.Now,
                    Description = "Some description",
                    Id = 2,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "my title"
                },
                new Article()
                {
                    Date = DateTime.Now,
                    Description = "Some description",
                    Id = 3,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "my title"
                }
        };
        Article article = new Article()

        {
            Date = DateTime.Now,
            Description = "Some description",
            Id = 2,
            IdNewsProvider = 2,
            Link = "Test link",
            Title = "test title"
        };

        private Mock<IRepository<Article>> repository;

        /// <summary>
        /// Override initialize with binding.
        /// </summary>
        [OneTimeSetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll<Article>()).Returns(this.testData);
            MockKernel.GetMock<IRepository<Article>>().Setup(ad => ad.Add(It.IsAny<Article>())).Returns(this.article);
            repository = new Mock<IRepository<Article>>();
        }

        /// <summary>
        /// Test checking for existing.
        /// </summary>
        /// <param name="title">
        /// Article title.
        /// </param>
        [Category("ArticleService")]
        [TestCase("my title", TestName = "MustBeTrue")]
        public void CheckDoesExistsTest(string title)
        {
            bool actual = MockKernel.Get<ArticleService>().CheckDoesExists(title);
            bool expected = this.testData.Count(x => x.Title == title) > 0;

            Assert.AreEqual(expected, actual);
        }

        [TestCase()]
        public void AddArticleTest()
        {
            Article actual = MockKernel.Get<IArticleService>().AddArticle(article);
            Assert.AreEqual(article, actual);
        }

        [Test]
        public void should_find_simple_article_by_title()
        {
            //arrange
            const string testTitle = "my title";

            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll<Article>()).Returns(this.testData);
            var target = MockKernel.Get<ArticleService>();

            //act
            var result = target.Find(testTitle);
            //assert
            Assert.IsTrue(result.Count() == 2 && result.FirstOrDefault()?.Title == testTitle);
        }

    }
}
