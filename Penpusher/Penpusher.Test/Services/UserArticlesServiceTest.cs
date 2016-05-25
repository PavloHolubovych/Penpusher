using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Services;

namespace Penpusher.Test.Services
{
    /// <summary>
    /// Class for testing UserArticlesService.
    /// </summary>
    [TestFixture]
    public class UserArticlesServiceTest : TestBase
    {
        /// <summary>
        /// The initialize.
        /// </summary>
        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IUsersArticlesService>().To<UsersArticlesService>();
            MockKernel.GetMock<IRepository<UsersArticle>>().Reset();
        }

        /// <summary>
        /// The get users read articles test.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        [Category("UserArticlesService")]
        [TestCase(1, TestName = "For existing user, that has read articles")]
        [TestCase(3, TestName = "For existing user, that hasn't read articles")]
        [TestCase(5, TestName = "For not existing user")]
        public void GetUsersReadArticlesTest(int userId)
        {
            var testArticles = new List<UsersArticle>()
            {
                new UsersArticle() { Id = 1, UserId = 1, ArticleId = 5, IsRead = false },
                new UsersArticle() { Id = 2, UserId = 1, ArticleId = 15, IsRead = true },
                new UsersArticle() { Id = 3, UserId = 2, ArticleId = 15, IsRead = true },
                new UsersArticle() { Id = 4, UserId = 2, ArticleId = 6, IsRead = false },
                new UsersArticle() { Id = 4, UserId = 3, ArticleId = 6, IsRead = false }
            };

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            IEnumerable<UsersArticle> actual = MockKernel.Get<IUsersArticlesService>().GetUsersReadArticles(userId);
            IEnumerable<UsersArticle> expected = testArticles.Where(ua => ua.IsRead == true && ua.UserId == userId);
            Assert.AreEqual(expected, actual);
        }

        [Category("UserArticlesService")]
        [TestCase(1, 2, TestName = "For existing user, that has favorite articles")]
        [TestCase(3, 2, TestName = "For existing user, that hasn't favorite articles")]
        [TestCase(5, 2, TestName = "For nt existing user")]
        public void GetUsersFavoriteArticlesTest(int userId, int expected)
        {
            var testArticles = new List<UsersArticle>()
            {
                new UsersArticle()
                {
                    Id = 1,
                    UserId = 1,
                    ArticleId = 5,
                    IsFavorite = false,
                    Article =new Article()
                    {
                        Id = 5,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle()
                {
                    Id = 2,
                    UserId = 1,
                    ArticleId = 153,
                    IsFavorite = true,
                    Article = new Article()
                    {
                        Id = 153,
                        Title = "second",
                        Date = DateTime.Now,
                        Description = "secondsecondsecondsecond",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle()
                {
                    Id = 3,
                    UserId = 1,
                    ArticleId = 15,
                    IsFavorite = true,
                    Article = new Article()
                    {
                        Id = 15,
                        Title = "second",
                        Date = DateTime.Now,
                        Description = "secondsecondsecondsecond",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle()
                {
                    Id = 4,
                    UserId = 1,
                    ArticleId = 6,
                    IsFavorite = false,
                    Article = new Article()
                    {
                        Id = 15,
                        Title = "third",
                        Date = DateTime.Now,
                        Description = "thirdthirdthirdthird",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle()
                {
                    Id = 4,
                    UserId = 3,
                    ArticleId = 63,
                    IsFavorite = false,
                    Article = new Article()
                    {
                        Id = 15,
                        Title = "fourth",
                        Date = DateTime.Now,
                        Description = "fourthfourthfourthfourth",
                        Image = "link",
                        Link = "linklink"
                    }
                }
            };

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            int actual = MockKernel.Get<IUsersArticlesService>().GetUsersFavoriteArticles(userId).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 5, TestName = "For existin UserArticle item with IsRead==false")]
        [TestCase(1, 15, TestName = "For existin UserArticle item with IsRead==true")]
        [TestCase(1, 7, TestName = "For not existin UserArticle")]
        public void MarkAsReadTest (int userId, int articleId)
        {
            var testArticles = new List<UsersArticle>()
            {
                new UsersArticle() { Id = 1, UserId = 1, ArticleId = 5, IsRead = false },
                new UsersArticle() { Id = 2, UserId = 1, ArticleId = 15, IsRead = true },
                new UsersArticle() { Id = 3, UserId = 2, ArticleId = 15, IsRead = true },
                new UsersArticle() { Id = 4, UserId = 2, ArticleId = 6, IsRead = false },
                new UsersArticle() { Id = 4, UserId = 3, ArticleId = 6, IsRead = false }
            };

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            MockKernel.GetMock<IRepository<UsersArticle>>()
                .Setup(edit => edit.Edit(It.IsAny<UsersArticle>()))
                .Callback((UsersArticle article) => { testArticles.Add(article); });
            MockKernel.Get<IUsersArticlesService>().MarkAsRead(userId, articleId);

            UsersArticle actual = MockKernel.Get<IUsersArticlesService>().GetUsersReadArticles(userId).First(ua => ua.ArticleId == articleId && ua.ArticleId == articleId);
            Assert.AreEqual(true, actual.IsRead);
        }

        [Category("UserArticlesService")]
        [TestCase(2, 6, TestName = "For existing UserArticle item with IsFavorite==false")]
        [TestCase(1, 5, TestName = "For existing UserArticle item with IsFavorite==true")]
        [TestCase(2, 5, TestName = "For not existing UserArticle")]
        public void AddToFavoritesTest(int userId, int articleId)
        {
            var testArticles = new List<UsersArticle>()
            {
                new UsersArticle() { Id = 1, UserId = 1, ArticleId = 5, IsRead = false, IsFavorite = true},
                new UsersArticle() { Id = 2, UserId = 1, ArticleId = 15, IsRead = true, IsFavorite = true },
                new UsersArticle() { Id = 3, UserId = 2, ArticleId = 15, IsRead = true, IsFavorite = false },
                new UsersArticle() { Id = 4, UserId = 2, ArticleId = 6, IsRead = false, IsFavorite = false },
                new UsersArticle() { Id = 4, UserId = 3, ArticleId = 6, IsRead = false, IsFavorite = false }
            };

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            MockKernel.GetMock<IRepository<UsersArticle>>()
                .Setup(edit => edit.Edit(It.IsAny<UsersArticle>()))
                .Callback((UsersArticle article) => { testArticles.Add(article); });

            MockKernel.Get<IUsersArticlesService>().AddToFavorites(userId, articleId);
            UsersArticle actual = testArticles.First(ua => ua.ArticleId == articleId && ua.ArticleId==articleId);
            Assert.AreEqual(actual.IsFavorite, true);
        }

        [Category("UserArticlesService")]
        [TestCase(1, 15, TestName = "For existing UserArticle item where article IsFavorite == true")]
        [TestCase(3, 6, TestName = "For existing UserArticle item where article IsFavorite == false")]
 
        public void RemoveFromFavoritesTest(int userId, int articleId)
        {
            var testArticles = new List<UsersArticle>()
            {
                new UsersArticle() { Id = 1, UserId = 1, ArticleId = 5, IsRead = false, IsFavorite = true},
                new UsersArticle() { Id = 2, UserId = 1, ArticleId = 15, IsRead = true, IsFavorite = true },
                new UsersArticle() { Id = 3, UserId = 2, ArticleId = 15, IsRead = true, IsFavorite = false },
                new UsersArticle() { Id = 4, UserId = 2, ArticleId = 6, IsRead = false, IsFavorite = false },
                new UsersArticle() { Id = 4, UserId = 3, ArticleId = 6, IsRead = false, IsFavorite = false }
            };

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            MockKernel.GetMock<IRepository<UsersArticle>>()
                .Setup(edit => edit.Edit(It.IsAny<UsersArticle>()))
                .Callback((UsersArticle article) =>
                {
                    var articleForChange = testArticles.First(ua => ua.ArticleId == article.ArticleId && 
                    ua.UserId == article.UserId);
                    articleForChange = article;
                });

            MockKernel.Get<IUsersArticlesService>().RemoveFromFavorites(userId, articleId);
            UsersArticle actual = testArticles.First(ua => ua.ArticleId == articleId && ua.ArticleId == articleId);
            Assert.AreEqual(false, actual.IsFavorite);
        }
    }
}
