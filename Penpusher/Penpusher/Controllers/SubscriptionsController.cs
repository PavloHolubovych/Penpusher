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
        [Route("getallsubscription/{id}")]
        //DEFECT: violation dependency inversion principle. method type must be more abstract and return is more specific
        public UserNewsProviderModels[] GetByUser(int id)
        {
            return newsProviderService.GetUserNewsProviderByUserId(id).ToArray();
        }

        public NewsProvider GetProviderDetails(int providerId)
        {
            return newsProviderService.GetAll().First(np => np.Id == providerId);
        }

        [Route("add")]
        public void Post(NewsProvider newsProvider)
        {
            string link = newsProvider.Link;
            newsProviderService.Subscription(link);
        }

        public bool SubscribeUserToProvider(int userId, int providerId)
        {
            return userProviderService.SubscribeUserToProvider(userId, providerId, true);
        }

        public bool UnsubscribeUserToProvider(int userId, int providerId)
        {
            return userProviderService.SubscribeUserToProvider(userId, providerId, false);
        }

        //DEFECT: its bad practice to return void in API. return bool at least to check does action perform successfully
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            newsProviderService.Unsubscription(id);
        }
    }
}