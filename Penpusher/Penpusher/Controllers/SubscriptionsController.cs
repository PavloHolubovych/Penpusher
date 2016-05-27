using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class SubscriptionsController : ApiController
    {
        private readonly INewsProviderService newsProviderService;
        private readonly IUserProviderService userProviderService;

        public SubscriptionsController(INewsProviderService newsProviderService, IUserProviderService userProviderService)
        {
            this.newsProviderService = newsProviderService;
            this.userProviderService = userProviderService;
        }

        [HttpGet]
        public IEnumerable<NewsProvider> GetAllNewsProviders()
        {
            return newsProviderService.GetAll();
        }

        public IEnumerable<UserNewsProviderModels> GetByUser(int id)
        {
            return newsProviderService.GetSubscriptionsByUserId();
        }

        public NewsProvider GetProviderDetails(int providerId)
        {
            return newsProviderService.GetAll().First(np => np.Id == providerId);
        }

        public void Post(NewsProvider newsProvider)
        {
            string link = newsProvider.Link;
            newsProviderService.Subscription(link);
        }

        [HttpPost]
        public bool SubscribeUserToProvider(int providerId)
        {
            return userProviderService.SubscribeUserToProvider(providerId, true);
        }

        [HttpPost]
        public bool UnsubscribeUserToProvider(int providerId)
        {
            return userProviderService.SubscribeUserToProvider(providerId, false);
        }

        [HttpGet]
        public bool IsUserSubscriberToProvider(int providerId)
        {
            bool p = userProviderService.IsUserSubscribedOnProvider(providerId);
            return p;
        }

        public UsersNewsProvider Delete(int id)
        {
            return newsProviderService.Unsubscription(id);
        }
    }
}