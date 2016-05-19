using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services
{
    public interface INewsProviderService
    {
        IEnumerable<NewsProvider> GetAll();

        IEnumerable<UserNewsProviderModels> GetByUserId(int id);

        NewsProvider AddNewsProvider(NewsProvider newsProvider);

        UsersNewsProvider AddSubscription(string link);

        void DeleteNewsProvider(int id);
    }
}