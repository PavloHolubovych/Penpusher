using System;
using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services
{
    public interface INewsProviderService
    {
        IEnumerable<NewsProvider> GetAll();

        NewsProvider GetById(int id);

        IEnumerable<UserNewsProviderModels> GetSubscriptionsByUserId(int id);

        UsersNewsProvider Subscription(string link);

        void Unsubscription(int id);

        bool UpdateLastBuildDateForNewsProvider(int id, DateTime? lastBuildDate);
    }
}