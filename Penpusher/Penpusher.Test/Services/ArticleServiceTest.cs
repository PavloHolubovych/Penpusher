/* TODO:  Usings should be outside of namespace
    remove redundand usings

    Fix stylecop warnings

    There is no need to put comments everywhere, code should be readable and undestandable. Write comments only for interfaces

    All test for 1 service should have the same category
    */
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

        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            //ToDO: bind to the interface here
            this.MockKernel.Bind<ArticleService>().ToSelf();
            this.MockKernel.GetMock<IRepository<Article>>().Reset();
        }

        [Category("ArticleService")]
        // TODO: word "test" is redundant in the testcase name. Name here should be method name(CheckDoesExists) plus title exists/title is missing 
        [TestCase("my title", TestName = "Test with exist title")]
        [TestCase("my title123321", TestName = "Test with not exist title")]
        public void CheckDoesExistsTest(string title)
        {
            /* TODO:  make initialization smaller (article creation can be put in 1 line)*/
            var testArticles = new List<Article>()
            {
                new Article()
                {
                    Id = 1,
                    Title = "test title"
                },
                new Article()
                {
                    Id = 2,
                    Title = "my title"
                },
                new Article()
                {
                    Id = 3,
                    Title = "my title"
                }
            };

            this.MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll<Article>()).Returns(testArticles);

            bool actual = this.MockKernel.Get<ArticleService>().CheckDoesExists(title);
            // TODO: exists method could be used here as well as in the service. It would be better to move expected to a parameter in the TestCase
            bool expected = testArticles.Count(x => x.Title == title) > 0;

            Assert.AreEqual(expected, actual);
        }

        //TODO Add name
        [TestCase()]
        public void AddArticleTest()
        {
            Article testArticle = new Article { Id = 1, Title = "newArticle" };
            this.MockKernel.GetMock<IRepository<Article>>().Setup(ad => ad.Add(It.Is<Article>(a => a.Id == 1))).Returns(testArticle);

            Article actual = this.MockKernel.Get<ArticleService>().AddArticle(testArticle);
            this.MockKernel.GetMock<IRepository<Article>>().Verify(r => r.Add(It.Is<Article>(a => a.Id == 1)), Times.Once);

            Assert.AreEqual(testArticle, actual, "Another article");
        }

        /// <summary>
        /// The find article test.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="expectedCount">
        /// The expected count.
        /// </param>
        [Category("ArticleService")]
        [TestCase("article1", 1, TestName = "Should find article by title")]
        [TestCase("article 1", 1, TestName = "Should find article by title contains space")]
        [TestCase("article2", 1, TestName = "Should find first article")]
        [TestCase("", 0, TestName = "Blank title article")]
        [TestCase("aticle3", 0, TestName = "Not exist article")]
        [TestCase(default(string), 0, TestName = "Not title is null")]
        public void FindArticleTest(string title, int expectedCount)
        {
            //arrange
            var testArticles = new List<Article>
            {
                new Article { Id = 1, Title = "article2"},
                new Article { Id = 2, Title = "article 1"},
                new Article { Id = 3, Title = "article1"}
            };

            // TODO: underscore is not a valit variable name, even in the lambda-expressions, use first letters from words instead(i.e. rm (RepositoryMock))
            MockKernel.GetMock<IRepository<Article>>().Setup(_ => _.GetAll<Article>()).Returns(testArticles);
            //TODO: variable declaration here is redundant
            var target = MockKernel.Get<ArticleService>();

            //act
            var result = target.Find(title);

            //assert
            //TODO: Assert should be as easy as possible(so compare expected with actual here, without logic)
            Assert.IsTrue(result.Count() == expectedCount);
        }
    }
}
