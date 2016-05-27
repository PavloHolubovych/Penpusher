using System;
using System.Collections.Generic;
using System.Linq;
using Penpusher.Models;

namespace Penpusher.Services
{
    public class NewsProviderService : INewsProviderService
    {
        private readonly IRepository<NewsProvider> newsProviderRepository;

        private readonly IRepository<UsersNewsProvider> usersNewsProviderRepository;

        private const int constIdUser = 5;

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

            return newsproviders; // ToList()?
        }

        public NewsProvider GetById(int id)
        {
           return newsProviderRepository.GetById(id);
        }

        public IEnumerable<UserNewsProviderModels> GetSubscriptionsByUserId(int id)
        {
            IEnumerable<UserNewsProviderModels> news = usersNewsProviderRepository.GetAll().Where(unp => unp.IdUser == id)
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
            return news;//.ToList()?
        }
        //i dont really like void type for methods changing db state
        public void Unsubscription(int id)
        {
            usersNewsProviderRepository.Delete(id);
        }

        public UsersNewsProvider Subscription(string link)
        {
            NewsProvider channel = newsProviderRepository.GetAll().FirstOrDefault(rm => rm.Link == link);

            if (channel == null)
            {
                channel = new NewsProvider
                {
                    Link = link,
                    Name = "test",//duplication. extract constant and reuse it
                    Description = "test",
                    SubscriptionDate = DateTime.Today
                };

                channel = newsProviderRepository.Add(channel);
            }

            UsersNewsProvider subscription = usersNewsProviderRepository.GetAll().FirstOrDefault(rm => rm.IdNewsProvider == channel.Id && rm.IdUser == constIdUser);
            return subscription ?? usersNewsProviderRepository.Add(new UsersNewsProvider
            {
                IdNewsProvider = channel.Id,
                IdUser = constIdUser
            });
        }

        //i dont really like void type for methods changing db state
        public void UpdateLastBuildDateForNewsProvider(int id, DateTime? lastBuildDate)
        {
            NewsProvider updatedProvider = newsProviderRepository.GetById(id);
            updatedProvider.LastBuildDate = lastBuildDate;
            newsProviderRepository.Edit(updatedProvider);
        }
    }
}