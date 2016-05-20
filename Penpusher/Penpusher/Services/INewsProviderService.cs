// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewsProviderService.cs" company="">
// </copyright>
// <summary>
//   The NewsProviderService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// The NewsProviderService interface.
    /// </summary>
    public interface INewsProviderService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        IEnumerable<NewsProvider> GetAll();

        /// <summary>
        /// The get by user id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        IEnumerable<UserNewsProviderModels> GetByUserId(int id);

        /// <summary>
        /// The add subscription.
        /// </summary>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <returns>
        /// The <see cref="UsersNewsProvider"/>.
        /// </returns>
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