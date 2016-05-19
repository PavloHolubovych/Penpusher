using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Test.Services
{
    [TestFixture]
    public class NewsProviderTest : TestBase
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            MockKernel.Bind<INewsProviderService>().To<NewsProviderService>();
            MockKernel.GetMock<IRepository<UsersNewsProvider>>().Reset();
        }

        [Category("NewsProviderService")]
        [TestCase(1, 2, TestName = "Get subscription for user 1")]
        [TestCase(2, 1, TestName = "Get subscription for user 2")]
        [TestCase(1, 3, TestName = "Get subscription for user 1(false)")]
        [TestCase(0, 3, TestName = "Get subscription for  undefined user")]
        public void GetByUserIdTest(int userid, int expectedCount)
        {
            // arrange
            var usernewsprovider = new List<UsersNewsProvider>()
            {
                new UsersNewsProvider
                {
                    Id = 1,
                    IdNewsProvider = 1,
                    IdUser = 1,
                    NewsProvider =
                        new NewsProvider()
                        {
                            Id = 1, Description = "firstfirstfirstfirstfirst", Name = "first"
                        }
                },
                new UsersNewsProvider
                {
                    Id = 2,
                    IdNewsProvider = 1,
                    IdUser = 2,
                    NewsProvider =
                        new NewsProvider()
                        {
                            Id = 2, Description = "secondsecondsecondsecondsecond", Name = "second"
                        }
                },
                new UsersNewsProvider
                {
                    Id = 3,
                    IdNewsProvider = 3,
                    IdUser = 1,
                    NewsProvider =
                        new NewsProvider()
                        {
                            Id = 3, Description = "thirdthirdthirdthirdthird", Name = "third"
                        }
                }
            };

            MockKernel.GetMock<IRepository<UsersNewsProvider>>().Setup(rm => rm.GetAll()).Returns(usernewsprovider);

            // act
            IEnumerable<UserNewsProviderModels> result = MockKernel.Get<INewsProviderService>().GetByUserId(userid);

            // assert
            int expected = result.Count();
            Assert.AreEqual(expected, expectedCount);
        }

        [Category("NewsProviderService")]
        [TestCase(1, TestName = "Delete subscription with id 0")]
        public void DeleteTest(int id)
        {
            // arrange
            var usernewsprovider = new List<UsersNewsProvider>()
            {
                new UsersNewsProvider
                {
                    Id = 1, IdNewsProvider = 1, IdUser = 1
                },
                new UsersNewsProvider
                {
                    Id = 2, IdNewsProvider = 1, IdUser = 2
                },
                new UsersNewsProvider
                {
                    Id = 3, IdNewsProvider = 3, IdUser = 1
                }
            };

            MockKernel.GetMock<IRepository<UsersNewsProvider>>().Setup(rm => rm.GetAll()).Returns(usernewsprovider);

            // act
            MockKernel.Get<INewsProviderService>().DeleteNewsProvider(id);

            IEnumerable<UserNewsProviderModels> result = MockKernel.Get<INewsProviderService>().GetByUserId(0);

            // arrange
            Assert.IsEmpty(result);
        }
    }
}