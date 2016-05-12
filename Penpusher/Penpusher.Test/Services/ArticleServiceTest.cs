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
                    Date = DateTime.Parse("10-04-2015"),
                    Description = "Some description",
                    Id = 1,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "test title"
                },
                new Article()
                {
                    Date = DateTime.Parse("20-03-2016"),
                    Description = "Some description",
                    Id = 2,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "my title"
                },
                new Article()
                {
                    Date = DateTime.Parse("18-03-2016"),
                    Description = "Some description",
                    Id = 3,
                    IdNewsProvider = 1,
                    Link = "Test link",
                    Title = "my title"
                }
        };

        /// <summary>
        /// Override initialize with binding.
        /// </summary>
        [OneTimeSetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<IArticleService>().To<ArticleService>();
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll<Article>()).Returns(this.testData);
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
        [TestFixture]
        public class ArticleFindTest //: TestBase
        {
            private Mock<IRepository<Article>> repository;

            [OneTimeSetUp]
            public void Initialize()
            {
                //    base.Initialize();
                repository = new Mock<IRepository<Article>>();
            }

            [Test]
            public void should_find_simple_article_by_title()
            {
                //arrange
                const string testTitle = "asda";
                var articles = new List<Article>
            {
                new Article
                {
                    Title = testTitle
                },
                new Article
                {
                    Title = "qwerty"
                }

            };

                var target = new ArticleService(repository.Object);
                repository.Setup(repository => repository.GetAll<Article>())
                    .Returns(articles);
                //act
                var result = target.Find(testTitle);
                //assert
                Assert.IsTrue(result.Count() == 1 && result.FirstOrDefault()?.Title == testTitle);
            }
        }
    }
}
