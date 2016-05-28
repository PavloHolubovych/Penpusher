using System.Collections.Generic;
using System.Web.Http;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;
        private readonly IUsersArticlesService _userArticlesService;
        private readonly INewsProviderService _newsProviderService;

        public ArticlesController(
            IArticleService articleService,
            IUsersArticlesService userArticlesService,
            INewsProviderService newsProviderService)
        {
            _articleService = articleService;
            _userArticlesService = userArticlesService;
            _newsProviderService = newsProviderService;
        }

        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return _articleService.GetArticlesFromProvider(newsProviderId);
        }

        [HttpPost]
        public UsersArticle MarkAsRead([FromBody] ArticleModel data)
        {
            return _userArticlesService.MarkAsRead(data.ArticleId);
        }

        [HttpGet]
        public IEnumerable<Article> UserReadArticles()
        {
            return _userArticlesService.GetUsersReadArticles();
        }

        [HttpGet]
        public IEnumerable<Article> UserFavoriteArticles()
        {
            return _userArticlesService.GetUsersFavoriteArticles();
        }

        [HttpGet]
        [ActionName("GetArticleDetail")]
        public Article ArticleDetails(int articleId)
        {
            return _articleService.GetById(articleId);
        }

        [HttpGet]
        public IEnumerable<Article> ArticlesFromSelectedProviders()
        {
            IEnumerable<UserNewsProviderModels> newsProviders = _newsProviderService.GetSubscriptionsByUserId();
            return _articleService.GetArticlesFromSelectedProviders(newsProviders);
        }

        [HttpPost]
       public void AddRemoveFavorites([FromBody] ArticleModel data)
        {
            _userArticlesService.AddRemoveFavorites(data.ArticleId, data.Flag);
        }

        [HttpGet]
        public UsersArticle UserArticleInfo(int articleIdInfo)
        {
            return _userArticlesService.UserArticleInfo(articleIdInfo);
        }

        [HttpPost]
        public UsersArticle ToReadLater([FromBody] ArticleModel data)
        {
            return _userArticlesService.ToReadLater(data.ArticleId, data.Flag);
        }

        public IEnumerable<Article> GetReadLeaterArticles()
        {
            return _userArticlesService.GetReadLaterArticles();
        }
    }
}