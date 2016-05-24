using System;
using System.Collections.Generic; 
using System.Web.Mvc;
using System.Xml.Linq;
using Penpusher.Models;
using Penpusher.Services.ContentService;

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

        /// <summary>
        /// The articles by subscription.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
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

        public ActionResult ArticleContentDetails(int articleId)
        {
            ViewBag.articleId = articleId;
            return View(articleId);
        }

    }
}