using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;
        private readonly IUsersArticlesService _userArticlesService;

        public ArticlesController(IArticleService articleService, IUsersArticlesService userArticlesService)
        {
            _articleService = articleService;
            _userArticlesService = userArticlesService;
        }

        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return _articleService.GetArticlesFromProvider(newsProviderId);
        }

        [HttpGet]
        public IEnumerable<Article> UserReadArticles(int userId)
        {
            var readArticles = _articleService.GetAllArticleses()
                .Join(
                    _userArticlesService.GetUsersReadArticles(userId),
                    article => article.Id,
                    readArticle => readArticle.ArticleId,
                    (article, readArticle) => article);

            return readArticles;
        }

        [HttpGet]
        [ActionName("GetArticleDetail")]
        public  Article ArticleDetails(int articleId)
        {
           return  _articleService.GetById(articleId);
        }

        [HttpGet]
        [ActionName("ArticlesFromSelectedProviders")]
        public IEnumerable<Article> ViewAllArticlesFromSelectedProviders(IEnumerable<NewsProvider> newsProviders)
        {
            return _articleService.GetArticlesFromSelectedProviders(newsProviders);
        }
    }
}
