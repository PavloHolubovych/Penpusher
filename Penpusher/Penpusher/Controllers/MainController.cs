// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainController.cs" company="Sigma software">
//   MainController
// </copyright>
// <summary>
//   The main controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        public MainController()
        {
            ViewBag.UserName = "UserNAme1";
            ViewBag.ProvidersList = new List<string> { "sdfs", "sdfsdf", "sdfsdf" };
        }

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

        /// <summary>
        /// The article content details.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult ArticleContentDetails()
        {
            return View();
        }
    }
}