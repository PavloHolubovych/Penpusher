// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionsController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SubscriptionsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Penpusher.Models;

namespace Penpusher.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Penpusher.Services;

    /// <summary>
    /// The subscriptions controller.
    /// </summary>
    [System.Web.Http.RoutePrefix("api")]
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
        /// The <see cref="NewsProvider[]"/>.
        /// </returns>
        [System.Web.Http.Route("getall")]
        public NewsProvider[] Get()
        {

            return _newsProviderService.GetAll().ToArray();
        }

        // GET api/<controller>/5
        [System.Web.Http.Route("getallsubscription/{id}")]
        public UserNewsProviderModels[] GetByUser(int id)
        {
            return _newsProviderService.GetByUserId(id).ToArray();
        }

        // POST api/<controller>

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="newsProvider">
        /// The news provider.
        /// </param>
        [System.Web.Http.Route("add")]
        public void Post(NewsProvider newsProvider)
        {
            var link = newsProvider.Link;
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
        [System.Web.Http.Route("delete/{id}")]

        public void Delete(int id)
        {
            _newsProviderService.Unsubscription(id);
        }
    }
}