using System.Web.Mvc;

namespace Penpusher.Controllers
{
    public class MainController : Controller
    {
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

        public ActionResult UserToReadLaterArticles()
        {
            return View();
        }

        public ActionResult UserFavoriteArticles()
        {
            return View();
        }

        public ActionResult ArticlesBySelectedSubscriptions()
        {
            return View();
        }

        //// try to avoid using VievBad and VievData. use model instead
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