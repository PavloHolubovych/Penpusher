using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Ninject;
using NUnit;
using NUnit.Framework;
using Penpusher;
using Penpusher.Services;

namespace Penpusher.Test
{
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
