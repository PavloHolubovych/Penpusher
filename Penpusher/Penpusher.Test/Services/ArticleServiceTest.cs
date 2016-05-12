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
            this.MockKernel.Bind<ArticleService>().ToSelf();
            this.MockKernel.GetMock<IRepository<Article>>().Reset();
        }
        
        [Category("ArticleService")]
        [TestCase("my title", TestName = "Test with exist title")]
        [TestCase("my title123321", TestName = "Test with not exist title")]
        public void CheckDoesExistsTest(string title)
        {
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
            bool expected =testArticles.Count(x => x.Title == title) > 0;

            Assert.AreEqual(expected, actual);
        }

        [TestCase()]
        public void AddArticleTest()
        {
            Article testArticle = new Article { Id = 1, Title = "newArticle" };

            this.MockKernel.GetMock<IRepository<Article>>().Setup(ad => ad.Add(It.Is<Article>(a => a.Id == 1))).Returns(testArticle);

            Article actual = this.MockKernel.Get<ArticleService>().AddArticle(testArticle);
            this.MockKernel.GetMock<IRepository<Article>>().Verify(r => r.Add(It.Is<Article>(a => a.Id == 1)), Times.Once);

            Assert.AreEqual(testArticle, actual, "Another article");
        }

        [Category("ArticleService")]
        [TestCase("article1", TestName = "Should find article by title") ]
        [TestCase("article2", TestName = "Should find first article")]
        [TestCase(null, TestName = "Blank title article")]
        public void FindArticleTest(string title)
        {
            var testArticles = new List<Article>()
            {
                new Article() { Id = 1, Title = "article2"},
                new Article() { Id = 2, Title = "article 1"},
                new Article() { Id = 3, Title = null},
                new Article() { Id = 4, Title = "article1"}
            };
            //arrange
            
            MockKernel.GetMock<IRepository<Article>>().Setup(asrv => asrv.GetAll<Article>()).Returns(testArticles);
            var target = MockKernel.Get<ArticleService>();

            //act
            var result = target.Find(title);

            //assert
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault()?.Title == title);
            Assert.IsTrue(result.Count() == 1);
        }

    }
}
