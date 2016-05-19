using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penpusher.Controllers
{
    public class MainController : Controller
    {
        public MainController()
        {
            ViewBag.UserName = "UserNAme1";
            ViewBag.ProvidersList = new List<string> { "sdfs", "sdfsdf", "sdfsdf" };
        }

        // GET: Main
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ArticlesBySubscription()
        {
            return View();
        }

        public ActionResult Subscriptions()
        {
            return View();
        }

        public ActionResult UserReadArticles()
        {
            return View();
        }
        public ActionResult ArticlesBySelectedSubscriptions()
        {
            return View();
        }

        public ActionResult ArticleContentDetails()
        {
            return View();
        }

    }
}