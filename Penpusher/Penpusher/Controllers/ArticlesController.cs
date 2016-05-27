using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService articleService;
        private readonly IUsersArticlesService userArticlesService;
        private readonly INewsProviderService newsProviderService;

        public ArticlesController(
            IArticleService articleService,
            IUsersArticlesService userArticlesService,
            INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.userArticlesService = userArticlesService;
            this.newsProviderService = newsProviderService;
        }

        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return articleService.GetArticlesFromProvider(newsProviderId);
        }

        [HttpPost]
        //// please do not use void. use bool instead
        public void MarkAsRead(int articleId)
        {
            userArticlesService.MarkAsRead(articleId);
        }

        [HttpGet]
        public IEnumerable<Article> UserReadArticles(int userId)
        {
            return userArticlesService.GetUsersReadArticles(userId);
        }

        [HttpGet]
        public IEnumerable<Article> UserFavoriteArticles(int userId)
        {
            return userArticlesService.GetUsersFavoriteArticles(userId);
        }

        [HttpGet]
        [ActionName("GetArticleDetail")]
        public Article ArticleDetails(int articleId)
        {
            return articleService.GetById(articleId);
        }

        [HttpGet]
        public IEnumerable<Article> ArticlesFromSelectedProviders(int userId)
        {
            IEnumerable<UserNewsProviderModels> newsProviders = newsProviderService.GetSubscriptionsByUserId(userId);
            return articleService.GetArticlesFromSelectedProviders(newsProviders);
        }

        [HttpPost]
       public void AddRemoveFavorites([FromBody]ArticleModel model)
        {
            userArticlesService.AddRemoveFavorites(model.ArticleId, model.Flag);
        }

        [HttpGet]
        public UsersArticle ReadLaterInfo(int articleIdInfo)
        {
            return userArticlesService.ReadLaterInfo(articleIdInfo);
        }

        [HttpPost]
        public UsersArticle ToReadLater( int articleIdRl, bool add)
        {
            return userArticlesService.ToReadLater( articleIdRl, add);
        }

        [HttpGet]
        public bool CheckIsFavorite(int articleId)
        {
            return userArticlesService.CheckIsFavorite(articleId);
        }

        public IEnumerable<Article> GetReadLeaterArticles()
        {
            var userId = 5;
            return userArticlesService.GetReadLaterArticles(userId);
        }
    }
}