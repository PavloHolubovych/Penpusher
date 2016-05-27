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
        //// DEFECT: violation dependency inversion principle. method type must be more abstract and return is more specific
        public IEnumerable<NewsProvider> GetAllNewsProviders()
        {
            return newsProviderService.GetAll();
        }

        // GET api/<controller>/5
        //DEFECT: violation dependency inversion principle. method type must be more abstract and return is more specific
        public UserNewsProviderModels[] GetByUser(int id)
        {
            return newsProviderService.GetSubscriptionsByUserId(id).ToArray();
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
            var p= userProviderService.IsUserSubscribedOnProvider(providerId);
            return p;
        }

        //DEFECT: its bad practice to return void in API. return bool at least to check does action perform successfully
  //      [Route("api/delete/{id}")]
        public void Delete(int id)
        {
            newsProviderService.Unsubscription(id);
        }
    }
}