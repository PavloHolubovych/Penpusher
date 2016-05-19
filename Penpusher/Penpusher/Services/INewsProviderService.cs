namespace Penpusher.Services
{
    using System.Collections.Generic;

    public interface INewsProviderService
    {
        
        IEnumerable<NewsProvider> GetAll();

        IEnumerable<NewsProvider> GetByUserId(int id);

        NewsProvider AddNewsProvider(NewsProvider newsProvider);

        UsersNewsProvider AddSubscription(string link);

        void DeleteNewsProvider(int id);
    }
}