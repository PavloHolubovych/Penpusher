using System;
using System.Collections.Generic; 
using System.Web.Mvc;
using System.Xml.Linq;
using Penpusher.Models;
using Penpusher.Services.ContentService;

namespace Penpusher.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;

    /// <summary>
    /// The main controller.
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult ArticlesBySubscription()
        {
            return View();
        }

        /// <summary>
        /// The subscriptions.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult Subscriptions()
        {
            return View();
        }

        /// <summary>
        /// The user read articles.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult UserReadArticles()
        {
            return View();
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult UserToReadLaterArticles()
        {
            return View();
        }


        /// <summary>
        /// The articles by selected subscriptions.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult ArticlesBySelectedSubscriptions()
        {

            return View();
        }

        public ActionResult ArticleContentDetails(int articleId)
        {
            ViewBag.articleId = articleId;
            ViewBag.userId = 5;
            return View(articleId);
        }

        public ActionResult ProviderDescription(int newsProviderId)
        {
            ViewBag.providerId = newsProviderId;
            return View();
        }
    }
}