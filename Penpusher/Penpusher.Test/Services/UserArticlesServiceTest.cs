using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Services;

namespace Penpusher.Test.Services
{
    [TestFixture]
    public class UserArticlesServiceTest : TestBase
    {
        private readonly List<UsersArticle> testArticles = new List<UsersArticle>
            {  
            new UsersArticle { Id = 1, UserId = Constants.UserId, ArticleId = 5, IsRead = false, IsFavorite = true, IsToReadLater = true, Article = new Article
                    {
                        Id = 5,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    } },
                new UsersArticle { Id = 2, UserId = Constants.UserId, ArticleId = 15, IsRead = true, IsFavorite = true, IsToReadLater = true, Article = new Article
                    {
                        Id = 15,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    }
},
                new UsersArticle { Id = 3, UserId = Constants.UserId, ArticleId = 15, IsRead = true, IsFavorite = false, IsToReadLater = false, Article = new Article
                    {
                        Id = 15,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    } },
                new UsersArticle { Id = 4, UserId = Constants.UserId, ArticleId = 6, IsRead = false, IsFavorite = false, IsToReadLater = true,  Article = new Article
                    {
                        Id = 6,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    } },
                new UsersArticle { Id = 4, UserId = Constants.UserId, ArticleId = 6, IsRead = false, IsFavorite = false, IsToReadLater = false, Article = new Article
                    {
                        Id = 6,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    } }
        };

        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IUsersArticlesService>().To<UsersArticlesService>();
            MockKernel.GetMock<IRepository<UsersArticle>>().Reset();
        }

        [Category("UserArticlesService")]
        [TestCase(TestName = "For existing user, that has read articles")]
         public void GetUsersReadArticlesTest( )
        {
            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            int expected = testArticles.Count(ua => ua.IsRead == true && ua.UserId == Constants.UserId);
            int actual = MockKernel.Get<IUsersArticlesService>().GetUsersReadArticles().Count();
            Assert.AreEqual(expected, actual);
        }

        [Category("UserArticlesService")]
        [TestCase( 2, TestName = "For existing user, that has favorite articles")]
        public void GetUsersFavoriteArticlesTest(int expected)
        {
            var testArticles2 = new List<UsersArticle>
            {
                new UsersArticle
                {
                    Id = 1,
                    UserId = 5,
                    ArticleId = 5,
                    IsFavorite = false,
                    Article = new Article
                    {
                        Id = 5,
                        Title = "first",
                        Date = DateTime.Now,
                        Description = "firstfirstfirst",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle
                {
                    Id = 2,
                    UserId = 5,
                    ArticleId = 153,
                    IsFavorite = true,
                    Article = new Article
                    {
                        Id = 153,
                        Title = "second",
                        Date = DateTime.Now,
                        Description = "secondsecondsecondsecond",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle
                {
                    Id = 3,
                    UserId = 1,
                    ArticleId = 15,
                    IsFavorite = true,
                    Article = new Article
                    {
                        Id = 15,
                        Title = "second",
                        Date = DateTime.Now,
                        Description = "secondsecondsecondsecond",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle
                {
                    Id = 4,
                    UserId = 1,
                    ArticleId = 6,
                    IsFavorite = false,
                    Article = new Article
                    {
                        Id = 15,
                        Title = "third",
                        Date = DateTime.Now,
                        Description = "thirdthirdthirdthird",
                        Image = "link",
                        Link = "linklink"
                    }
                },
                new UsersArticle
                {
                    Id = 4,
                    UserId = 5,
                    ArticleId = 63,
                    IsFavorite = true,
                    Article = new Article
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

            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles2);
            int actual = MockKernel.Get<IUsersArticlesService>().GetUsersFavoriteArticles().Count();
            Assert.AreEqual(expected, actual);
        }

        [Category("UserArticlesService")]
        [TestCase(5, TestName = "For existin UserArticle item with IsRead==false")]
        [TestCase(15, TestName = "For existin UserArticle item with IsRead==true")]
        public void MarkAsReadTest(int articleId)
        {
            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            MockKernel.GetMock<IRepository<UsersArticle>>()
                .Setup(edit => edit.Edit(It.IsAny<UsersArticle>()))
                .Callback((UsersArticle article) => { testArticles.Add(article); });
            MockKernel.Get<IUsersArticlesService>().MarkAsRead(articleId);

            bool actual = MockKernel.Get<IUsersArticlesService>().GetUsersReadArticles().Any(ua => ua.Id == articleId);
            Assert.AreEqual(true, actual);
        }

        [Category("UserArticlesService")]
        [TestCase( 6, true, TestName = "Add to favorite for existing UserArticle item with IsFavorite==false")]
        [TestCase( 5, true, TestName = "Add to favorite for existing UserArticle item with IsFavorite==true")]
        [TestCase( 5, true, TestName = "Add to favorite for not existing UserArticle")]
        [TestCase( 3, false, TestName = "Remove from existing UserArticle item with IsFavorite==false")]
        [TestCase( 2, false, TestName = "Remove from existing UserArticle item with IsFavorite==true")]
        [TestCase( 5, false, TestName = "Remove from not existing UserArticle")]
        public void AddRemoveFavoritesTest(int articleId, bool favoriteFlag)
        {
            MockKernel.GetMock<IRepository<UsersArticle>>().Setup(usrv => usrv.GetAll()).Returns(testArticles);
            MockKernel.GetMock<IRepository<UsersArticle>>()
                .Setup(edit => edit.Edit(It.IsAny<UsersArticle>()))
                .Callback((UsersArticle article) => { testArticles.Add(article); });

            MockKernel.Get<IUsersArticlesService>().AddRemoveFavorites(articleId, favoriteFlag);
            UsersArticle actual = testArticles.First(ua => ua.UserId == Constants.UserId && ua.ArticleId == articleId);
            Assert.AreEqual(favoriteFlag, actual.IsFavorite);
        }
    }
}