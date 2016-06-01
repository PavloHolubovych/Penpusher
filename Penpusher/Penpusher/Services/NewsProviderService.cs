using System;
using System.Collections.Generic;
using System.Linq;
using Penpusher.Models;

namespace Penpusher.Services
{
    public class NewsProviderService : INewsProviderService
    {
        private const int ConstIdUser = 5;
        private readonly IRepository<NewsProvider> newsProviderRepository;
        private readonly IRepository<UsersNewsProvider> usersNewsProviderRepository;

        public NewsProviderService(IRepository<NewsProvider> repository, IRepository<UsersNewsProvider> usersNewsProviderRepository)
        {
            newsProviderRepository = repository;
            this.usersNewsProviderRepository = usersNewsProviderRepository;
        }

        public IEnumerable<NewsProvider> GetAll()
        {
            IEnumerable<NewsProvider> newsproviders = newsProviderRepository.GetAll().Select(n => new NewsProvider
            {
                Id = n.Id,
                Name = n.Name,
                Description = n.Description,
                Link = n.Link,
                RssImage = n.RssImage,
                SubscriptionDate = n.SubscriptionDate,
                LastBuildDate = n.LastBuildDate
            });

            return newsproviders.ToList();
        }

        public NewsProvider GetById(int id)
        {
           return newsProviderRepository.GetById(id);
        }

        public IEnumerable<UserNewsProviderModels> GetSubscriptionsByUserId()
        {
            IEnumerable<UserNewsProviderModels> news = usersNewsProviderRepository.GetAll().Where(unp => unp.IdUser == Constants.UserId)
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
            return news.ToList();
        }

        public UsersNewsProvider Unsubscription(int id)
        {
            return usersNewsProviderRepository.Delete(id);
        }

        public UsersNewsProvider Subscription(string link, string name, string description)
        {
            NewsProvider channel = newsProviderRepository.GetAll().FirstOrDefault(rm => rm.Link == link);

            if (channel == null)
            {
                channel = new NewsProvider
                {
                    Link = link,
                    Name = name,
                    Description = description,
                    RssImage = "https://cdn3.iconfinder.com/data/icons/ballicons-reloaded-free/512/icon-70-512.png",
                    SubscriptionDate = DateTime.Today
                };

                channel = newsProviderRepository.Add(channel);
            }

            UsersNewsProvider subscription = usersNewsProviderRepository.GetAll()
                .FirstOrDefault(rm => rm.IdNewsProvider == channel.Id && rm.IdUser == ConstIdUser);
            return subscription ?? usersNewsProviderRepository.Add(new UsersNewsProvider
            {
                IdNewsProvider = channel.Id,
                IdUser = ConstIdUser
            });
        }

        public bool UpdateLastBuildDateForNewsProvider(int id, DateTime? lastBuildDate)
        {
            NewsProvider updatedProvider = newsProviderRepository.GetById(id);
            if (updatedProvider == null)
            {
                return false;
            }
            updatedProvider.LastBuildDate = lastBuildDate;
            newsProviderRepository.Edit(updatedProvider);
            return true;
        }
    }
}