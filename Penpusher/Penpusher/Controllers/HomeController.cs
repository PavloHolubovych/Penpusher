using System.Collections.Generic; 
using System.Web.Mvc; 
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;
        private readonly IUsersArticlesService usersArticlesService;


        public HomeController(IUsersArticlesService usersArticlesService, IArticleService articleService,
            INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
            this.usersArticlesService = usersArticlesService; 
        }

        public ActionResult Index()
        {
            ViewBag.UserName = "UserNAme1";
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
    }
}