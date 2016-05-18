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
        public override void Initialize()
        {
            base.Initialize();
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
            var actual = MockKernel.Get<IUsersArticlesService>().GetUsersReadArticles(userId);
            var expected = testArticles.Where(x => x.IsRead.Value == true && x.UserId == userId);

            Assert.AreEqual(expected, actual);
        }
    }
}
