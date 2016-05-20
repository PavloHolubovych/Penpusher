using System;
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

        /// <summary>
        /// The get subscription user test.
        /// </summary>
        /// <param name="userid">
        /// The title.
        /// </param>
        /// <param name="expectedCount">
        /// The expected Count.
        /// </param>
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
                new UsersNewsProvider {Id = 1,IdNewsProvider = 1,IdUser = 1,NewsProvider = new NewsProvider(){Id = 1,Description = "firstfirstfirstfirstfirst",Name = "first"}},
                new UsersNewsProvider{Id = 2,IdNewsProvider = 1,IdUser = 2,NewsProvider = new NewsProvider(){Id = 2,Description = "secondsecondsecondsecondsecond",Name = "second"}},
                new UsersNewsProvider{Id = 3,IdNewsProvider = 3,IdUser = 1,NewsProvider = new NewsProvider(){Id = 3,Description = "thirdthirdthirdthirdthird",Name = "third"}}
            };

            MockKernel.GetMock<IRepository<UsersNewsProvider>>().Setup(rm => rm.GetAll()).Returns(usernewsprovider);

            // act
            IEnumerable<UserNewsProviderModels> result = MockKernel.Get<INewsProviderService>().GetByUserId(userid);

            // assert
            int expected = result.Count();
            Assert.AreEqual(expected, expectedCount);
        }

        [Category("NewsProviderService")]
        [TestCase(0, TestName = "Delete subscription with id 0")]
        public void DeleteTest(int id)
        {
            // arrange
            var usernewsprovider = new List<UsersNewsProvider>()
            {
                new UsersNewsProvider {Id = 1, IdNewsProvider = 1, IdUser = 1},
                new UsersNewsProvider {Id = 2, IdNewsProvider = 1, IdUser = 2},
                new UsersNewsProvider {Id = 3, IdNewsProvider = 3, IdUser = 1}
            };

            MockKernel.GetMock<IRepository<UsersNewsProvider>>().Setup(rm => rm.GetAll()).Returns(usernewsprovider);

            //act
            MockKernel.Get<INewsProviderService>().DeleteNewsProvider(id);

            IEnumerable<UserNewsProviderModels> result = MockKernel.Get<INewsProviderService>().GetByUserId(id);

            // assert
            Assert.IsEmpty(result);
        }

        [Category("NewsProviderService")]
        [TestCase("link1", TestName = "Add new subscription")]

        public void AddSubscriptionTest(string link)
        {
            var channel = new NewsProvider { Link = "link1", Id = 1 };
            var subscription = new UsersNewsProvider
            {
                Id = 2,
                IdNewsProvider = 1,
                IdUser = 4
            };

            MockKernel.GetMock<IRepository<NewsProvider>>().Setup(repo => repo.GetAll()).Returns(new List<NewsProvider>
            {
                channel
            });

            MockKernel.GetMock<IRepository<UsersNewsProvider>>()
                .Setup(repo => repo.GetAll())
                .Returns(new List<UsersNewsProvider>
                {
                    new UsersNewsProvider {Id = 1, IdNewsProvider = 2, IdUser = 1}
                });
            MockKernel.GetMock<IRepository<UsersNewsProvider>>()
                .Setup(repos => repos.Add(It.IsAny<UsersNewsProvider>()))
                .Returns(subscription);
            //// act
            UsersNewsProvider actual = MockKernel.Get<INewsProviderService>().AddSubscription(link);

            ////assert
            Assert.AreEqual(actual.Id, subscription.Id);
            Assert.AreEqual(actual.IdNewsProvider, channel.Id);
        }

        [Category("NewsProviderService")]
        [TestCase("link1", TestName = "Add new subscription and channel")]
        //[TestCase("link3", TestName = "Add subscription that isn't in database")]

        public void AddSubscriptionAndChannelTest(string link)
        {
            var channel = new NewsProvider { Link = "link1", Id = 1 };
            var subscription = new UsersNewsProvider
            {
                Id = 2,
                IdNewsProvider = 1,
                IdUser = 4
            };

            Mock<IRepository<NewsProvider>> newsProviderRepositoryMock = MockKernel.GetMock<IRepository<NewsProvider>>();
            newsProviderRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<NewsProvider>());
            newsProviderRepositoryMock.Setup(repo => repo.Add(It.IsAny<NewsProvider>())).Returns(channel);

            MockKernel.GetMock<IRepository<UsersNewsProvider>>()
                .Setup(repo => repo.GetAll())
                .Returns(new List<UsersNewsProvider>
                {
                    new UsersNewsProvider {Id = 1, IdNewsProvider = 2, IdUser = 1}
                });
            MockKernel.GetMock<IRepository<UsersNewsProvider>>()
                .Setup(repos => repos.Add(It.IsAny<UsersNewsProvider>()))
                .Returns(subscription);
            //// act
            UsersNewsProvider actual = MockKernel.Get<INewsProviderService>().AddSubscription(link);

            ////assert
            Assert.AreEqual(actual.Id, subscription.Id);
            Assert.AreEqual(actual.IdNewsProvider, channel.Id);
            newsProviderRepositoryMock.Verify(repo => repo.Add(It.IsAny<NewsProvider>()), Times.Once);
        }

        [Category("NewsProviderService")]
        [TestCase("link1", TestName = "Add new subscription and channel")]

        public void NotAddSubscriptionIfExistsTest(string link)
        {
            var channel = new NewsProvider { Link = "link1", Id = 1 };
            var subscription = new UsersNewsProvider
            {
                Id = 2,
                IdNewsProvider = 1,
                IdUser = 4
            };

            Mock<IRepository<NewsProvider>> newsProviderRepositoryMock = MockKernel.GetMock<IRepository<NewsProvider>>();
            newsProviderRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<NewsProvider>());
            newsProviderRepositoryMock.Setup(repo => repo.Add(It.IsAny<NewsProvider>())).Returns(channel);

            Mock<IRepository<UsersNewsProvider>> subscriptionsRepository = MockKernel.GetMock<IRepository<UsersNewsProvider>>();
            subscriptionsRepository
                .Setup(repo => repo.GetAll())
                .Returns(new List<UsersNewsProvider>
                {
                    subscription
                });
            //// act
            UsersNewsProvider actual = MockKernel.Get<INewsProviderService>().AddSubscription(link);

            ////assert
            Assert.AreEqual(actual.Id, subscription.Id);
            Assert.AreEqual(actual.IdNewsProvider, channel.Id);
            subscriptionsRepository.Verify(repo => repo.Add(It.IsAny<UsersNewsProvider>()), Times.Never);
        }
    }
}
