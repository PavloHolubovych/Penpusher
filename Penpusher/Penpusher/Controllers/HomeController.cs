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
        private readonly INewsProviderService newsProviderService;


        public HomeController(IDbProvider dbProvider, IArticleService articleService, INewsProviderService newsProviderService)
        {
            this.dbProvider = dbProvider;
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
        }

        public void addNewsProvider()
        {
            newsProviderService.AddNewsProvider(new NewsProvider
            {
                Name = "etr",
                Description = "ert",
                Link = "ghgjg",
                RssImage = "image",
                SubscriptionDate = DateTime.Today
            });
        }

        public void delNewsProvider()
        {
            newsProviderService.DeleteNewsProvider(15);
        }

        public ActionResult Index()
        {
            ///delNewsProvider();

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