using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbProvider dbProvider;
        private readonly IArticleService articleService;


        public HomeController(IDbProvider dbProvider, IArticleService articleService)
        {
            this.dbProvider = dbProvider;
            this.articleService = articleService;
        }

        public ActionResult Index()
        {
           // articleService.AddArticle(new Article { Description = "saf", IdProvider = 1, Date = DateTime.Today, Link = "dsffdsf", Title = "sdfsd" });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
        public class SqlServerDbProvider : IDbProvider
        {

            public void Add(Item item)
            {


            }
        }

        public class Item
        {
        }
    }
}