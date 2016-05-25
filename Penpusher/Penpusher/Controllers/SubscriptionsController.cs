// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionsController.cs" company="Sigma software">
//   Subscription
// </copyright>
// <summary>
//   Defines the SubscriptionsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Penpusher.Models;

namespace Penpusher.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Services;

    /// <summary>
    /// The subscriptions controller.
    /// </summary>
    [RoutePrefix("api")]
    public class SubscriptionsController : ApiController
    {
        /// <summary>
        /// The _news provider service.
        /// </summary>
        private readonly INewsProviderService _newsProviderService;

        public SubscriptionsController(INewsProviderService newsProviderService)
        {
            _newsProviderService = newsProviderService;
        }

        // GET api/<controller>

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>NewsProvider[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        [Route("getall")]
        public NewsProvider[] Get()
        {
            return _newsProviderService.GetAll().ToArray();
        }

        // GET api/<controller>/5
        [Route("getallsubscription/{id}")]
        public UserNewsProviderModels[] GetByUser(int id)
        {
            return _newsProviderService.GetByUserId(id).ToArray();
        }

        public  NewsProvider GetProviderDetails(int providerId)
        {
           return  _newsProviderService.GetAll().First(np => np.Id == providerId);
        }

        // POST api/<controller>

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="newsProvider">
        /// The news provider.
        /// </param>
        [Route("add")]
        public void Post(NewsProvider newsProvider)
        {
            string link = newsProvider.Link;
            _newsProviderService.Subscription(link);
        }

        // PUT api/<controller>/5

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [Route("delete/{id}")]

        public void Delete(int id)
        {
            _newsProviderService.Unsubscription(id);
        }
    }
}