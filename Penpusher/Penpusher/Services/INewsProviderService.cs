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

    /// <summary>
    /// The NewsProviderService interface.
    /// </summary>
    public interface INewsProviderService
    {
        /// <summary>
        /// The add news provider. 
        /// </summary>
        /// <returns>
        /// The <see cref="NewsProvider"/>.
        /// </returns>
        IEnumerable<NewsProvider> GetAll();
        IEnumerable<UserNewsProviderModels> GetByUserId(int id);


        NewsProvider AddNewsProvider(NewsProvider newsProvider);
        UsersNewsProvider AddSubscription(string link);

        /// <summary>
        /// The delete news provider.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void DeleteNewsProvider(int id);
    }
}