// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewsProviderService.cs" company="">
//   
// </copyright>
// <summary>
//   The NewsProviderService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Penpusher.Models;

namespace Penpusher.Services
{
    using System.Collections.Generic;

    public interface INewsProviderService
    {
        
        IEnumerable<NewsProvider> GetAll();
        IEnumerable<UserNewsProviderModels> GetByUserId(int id);

        NewsProvider AddNewsProvider(NewsProvider newsProvider);

        UsersNewsProvider AddSubscription(string link);

        void DeleteNewsProvider(int id);
    }
}