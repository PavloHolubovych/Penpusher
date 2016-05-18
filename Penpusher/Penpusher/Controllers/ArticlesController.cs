using System.Collections.Generic;
using System.Web.Http;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return _articleService.GetArticlesFromProvider(newsProviderId);
        }
    }
}
