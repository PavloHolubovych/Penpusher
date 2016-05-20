using System;
using System.Collections.Generic;
using System.Linq;
using Penpusher.Models;

namespace Penpusher.Services
{
    public class NewsProviderService : INewsProviderService
    {
        private readonly IRepository<NewsProvider> _newsProviderRepository;

        private readonly IRepository<UsersNewsProvider> _usersNewsProviderRepository;

        public NewsProviderService(IRepository<NewsProvider> repository, IRepository<UsersNewsProvider> usersNewsProviderRepository)
        {
            _newsProviderRepository = repository;
            _usersNewsProviderRepository = usersNewsProviderRepository;
        }

        public IEnumerable<NewsProvider> GetAll()
        {
            IEnumerable<NewsProvider> newsprovider = _newsProviderRepository.GetAll().Select(n => new NewsProvider
            {
                Id = n.Id,
                Name = n.Name,
                Description = n.Description,
                Link = n.Link,
                RssImage = n.RssImage,
                SubscriptionDate = n.SubscriptionDate
            });

            return newsprovider;
        }

        public IEnumerable<UserNewsProviderModels> GetByUserId(int id)
        {
            IEnumerable<UserNewsProviderModels> news = _usersNewsProviderRepository.GetAll().Where(_ => _.IdUser == id)
                .Select(un => new UserNewsProviderModels
                {
                    Id = un.Id,
                    IdNewsProvider = un.NewsProvider.Id,
                    Name = un.NewsProvider.Name,
                    Description = un.NewsProvider.Description,
                    Link = un.NewsProvider.Link,
                    RssImage = un.NewsProvider.RssImage,
                    SubscriptionDate = un.NewsProvider.SubscriptionDate
                });
            return news;
        }

        public void DeleteNewsProvider(int id)
        {
            _usersNewsProviderRepository.Delete(id);
        }

        public UsersNewsProvider AddSubscription(string link)
        {
            NewsProvider channel = _newsProviderRepository.GetAll().FirstOrDefault(rm => rm.Link == link);

            if (channel == null)
            {
                channel = new NewsProvider
                {
                    Link = link,
                    Name = "test",
                    Description = "test",
                    SubscriptionDate = DateTime.Today
                };

                channel = _newsProviderRepository.Add(channel);
            }

            UsersNewsProvider subscription = _usersNewsProviderRepository.GetAll().FirstOrDefault(rm => rm.IdNewsProvider == channel.Id);
            return subscription ?? _usersNewsProviderRepository.Add(new UsersNewsProvider
            {
                IdNewsProvider = channel.Id,
                IdUser = 4
            });
        }
    }
}