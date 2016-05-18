// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProviderService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ProviderService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Penpusher.Services.Base;

    /// <summary>
    /// The provider service.
    /// </summary>
    public class NewsProviderService : INewsProviderService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<NewsProvider> repository;
        private readonly IRepository<UsersNewsProvider> usersNewsProvider;

        public NewsProviderService(IRepository<NewsProvider> repository, IRepository<UsersNewsProvider> usersNewsProvider)
        {
            this.repository = repository;
            this.usersNewsProvider = usersNewsProvider;
        }
        public IEnumerable<NewsProvider> GetAll()
        {
            IEnumerable<NewsProvider> newsprovider = from n in repository.GetAll()
                                                     select new NewsProvider()
                                                     {
                                                         Id = n.Id,
                                                         Name = n.Name,
                                                         Description = n.Description,
                                                         Link = n.Link,
                                                         RssImage = n.RssImage,
                                                         SubscriptionDate = n.SubscriptionDate

                                                     };

            return newsprovider;
        }

        public IEnumerable<NewsProvider> GetByUserId(int id)
        {
            IEnumerable<NewsProvider> news = from un in usersNewsProvider.GetAll().Where(_ => _.IdUser == id).ToList()
                select new NewsProvider()
                {
                    Id = un.Id,
                    Name = un.NewsProvider.Name,
                    Description = un.NewsProvider.Description,
                    Link = un.NewsProvider.Link,
                    RssImage = un.NewsProvider.RssImage,
                    SubscriptionDate = un.NewsProvider.SubscriptionDate

                };
            return news;
        }

        /// <summary>
        /// The add provider.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <returns>
        /// The <see cref="NewsProvider"/>.
        /// </returns>
        public NewsProvider AddNewsProvider(NewsProvider provider)
        {

            return repository.Add(provider);
        }

        /// <summary>
        /// The delete news provider.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void DeleteNewsProvider(int id)
        {
            usersNewsProvider.Delete(id);  
        }

        /// <summary>
        /// The add subscription news provider.
        /// </summary>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <returns>
        /// The <see cref="UsersNewsProvider"/>.
        /// </returns>
        {
            var channels = repository.GetAll().FirstOrDefault(rm => rm.Link == link);

            if (channels == null)
            {
                channels = new NewsProvider();
                // add channel
                AddNewsProvider(channels);
            }

            var b = usersNewsProvider.GetAll().FirstOrDefault(rm => rm.IdNewsProvider == channels.Id);

            if (b == null)
            {
                return usersNewsProvider.Add(b);
            }
            else
            {
                return new UsersNewsProvider();
            }
        }
    }

}