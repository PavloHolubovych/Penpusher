using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services
{
    public interface INewsProviderService
    {
        IEnumerable<NewsProvider> GetAll();

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

        void DeleteNewsProvider(int id);
    }
}