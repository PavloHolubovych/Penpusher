using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Penpusher.Models;

namespace Penpusher.Controllers
{
    public class WebApiTestController : ApiController
    {
        private IEnumerable<TestClassForApi> _items;       

        public WebApiTestController(Iitems items)
        {
            _items = items.GetAllItems();
        }
    }    
}
