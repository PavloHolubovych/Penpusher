﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    [System.Web.Http.RoutePrefix("api")]
    public class SubscriptionsController : ApiController
    {
        private readonly INewsProviderService _newsProviderService;


        public SubscriptionsController(INewsProviderService newsProviderService)
        {
            _newsProviderService = newsProviderService;
        }

        // GET api/<controller>
        [System.Web.Http.Route("getall")]
        public NewsProvider[] Get()
        {

            return _newsProviderService.GetAll().ToArray();
        }

        // GET api/<controller>/5
        [System.Web.Http.Route("getall2/{id}")]
        public NewsProvider[] GetByUser(int id)
        {
            return _newsProviderService.GetByUserId(id).ToArray();
        }

        // POST api/<controller>
        [System.Web.Http.Route("add")]
        public string Post(NewsProvider newsProvider)
        {
            var link = newsProvider.Link;
            return link;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [System.Web.Http.Route("delete/{id}")]

        public void Delete(int id)
        {
            _newsProviderService.DeleteNewsProvider(id);
        }
    }
}