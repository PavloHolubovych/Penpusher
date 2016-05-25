using System;
using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services
{
    public interface INewsProviderService
    {
        IEnumerable<NewsProvider> GetAll();

        IEnumerable<UserNewsProviderModels> GetByUserId(int id);

        UsersNewsProvider Subscription(string link);

        void Unsubscription(int id);

        void UpdateLastBuildDateForNewsProvider(int id, DateTime? lastBuildDate);
    }
}