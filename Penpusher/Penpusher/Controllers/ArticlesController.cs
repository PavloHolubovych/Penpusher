using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Article> ArticlesFromProvider(int idProvider)
        {
            var articles = _articleService.GetArticlesFromProvider(idProvider);
            return articles;
        }

    }

}
