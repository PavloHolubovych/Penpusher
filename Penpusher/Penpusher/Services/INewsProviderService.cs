// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewsProviderService.cs" company="">
//   
// </copyright>
// <summary>
//   The NewsProviderService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        /// <param name="newsProvider">
        /// The news provider.
        /// </param>
        /// <returns>
        /// The <see cref="NewsProvider"/>.
        /// </returns>
        IEnumerable<NewsProvider> GetAll();
        IEnumerable<UsersNewsProvider> GetByUserId();


        NewsProvider AddNewsProvider(NewsProvider newsProvider);

        /// <summary>
        /// The delete news provider.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void DeleteNewsProvider(int id);
    }
}