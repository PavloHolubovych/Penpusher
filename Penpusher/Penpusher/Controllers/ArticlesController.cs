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
        private readonly INewsProviderService _newsProviderService;

        public ArticlesController(IArticleService articleService, IUsersArticlesService userArticlesService,
            INewsProviderService newsProviderService)
        {
            this._articleService = articleService;
            this._userArticlesService = userArticlesService;
            this._newsProviderService = newsProviderService;
        }

        /// <summary>
        /// Get all articles by provider ID.
        /// </summary>
        /// <param name="newsProviderId">
        /// Provider ID
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return this._articleService.GetArticlesFromProvider(newsProviderId);
        }

        [HttpGet]
        public void MarkAsRead(int userId, int articleId)
        {
            _userArticlesService.MarkAsRead(userId, articleId);
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
        public IEnumerable<Article> ArticlesFromSelectedProviders(int someUserId)
        {
            var newsProviders = _newsProviderService.GetByUserId(someUserId);
            return _articleService.GetArticlesFromSelectedProviders(newsProviders);
        }

        [HttpPost] 
        public void AddToFavorites(int userId, int articleId)
        {
            _userArticlesService.AddToFavorites(userId, articleId);
        }

        [HttpPost]
        public void RemoveFromFavorites(int userId, int articleId)
        {
            _userArticlesService.RemoveFromFavorites(userId, articleId);
        }
        
        [HttpGet]
        public bool CheckIsFavorite(int userId, int articleId)
        {
            return _userArticlesService.CheckIsFavorite(userId, articleId);
        }
    }
}
