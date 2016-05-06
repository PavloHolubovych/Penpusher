using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDbProvider dbProvider;

        public HomeController()
        {
        }

        public HomeController(IDbProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
    class ItemRepository
    {
        private readonly IDbProvider dbProvider;
        //private IDbProvider dbProvider;
        public ItemRepository(IDbProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public void Add(Item item)
        {
            dbProvider.Add(item);
        }
    }

    public interface IDbProvider
    {
        void Add(Item item);
    }
    class SqlServerDbProvider : IDbProvider
    {

        public void Add(Item item)
        {

        }
    }

    public class Item
    {
    }
}
