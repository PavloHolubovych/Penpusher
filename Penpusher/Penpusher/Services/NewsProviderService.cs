using System;
using System.Collections.Generic;
using System.Linq;
using Penpusher.Models;

namespace Penpusher.Services
{
    public class NewsProviderService : INewsProviderService
    {
        private readonly IRepository<NewsProvider> newsProviderRepository;

        private readonly IRepository<UsersNewsProvider> usersNewsProvider;

        public NewsProviderService(IRepository<NewsProvider> repository, IRepository<UsersNewsProvider> usersNewsProvider)
        {
            newsProviderRepository = repository;
            this.usersNewsProvider = usersNewsProvider;
        }

        public IEnumerable<NewsProvider> GetAll()
        {
            IEnumerable<NewsProvider> newsprovider = newsProviderRepository.GetAll().Select(n => new NewsProvider
            {
                Id = n.Id, Name = n.Name, Description = n.Description, Link = n.Link, RssImage = n.RssImage, SubscriptionDate = n.SubscriptionDate
            });

            return newsprovider;
        }

        public IEnumerable<UserNewsProviderModels> GetByUserId(int id)
        {
            IEnumerable<UserNewsProviderModels> news = usersNewsProvider.GetAll().Where(_ => _.IdUser == id)
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

        public NewsProvider AddNewsProvider(NewsProvider provider)
        {
            return newsProviderRepository.Add(provider);
        }

        public void DeleteNewsProvider(int id)
        {
            usersNewsProvider.Delete(id);
        }

        public UsersNewsProvider AddSubscription(string link)
        {
            NewsProvider channel = newsProviderRepository.GetAll().FirstOrDefault(rm => rm.Link == link);

            if (channel == null)
            {
                channel = new NewsProvider
            {
                    Link = link, Name = "test", Description = "test", SubscriptionDate = DateTime.Today
                };

                channel = AddNewsProvider(channel);
            }

            int subscription = usersNewsProvider.GetAll().Count(rm => rm.IdNewsProvider == channel.Id);

            if (subscription == 0)
            {
                return usersNewsProvider.Add(new UsersNewsProvider
                {
                    IdNewsProvider = channel.Id,
                    IdUser = 4
                });
            }

                return new UsersNewsProvider();
            }
        }
}