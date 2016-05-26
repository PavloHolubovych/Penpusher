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
        public void MarkAsRead(int userId, int articleId)
        {
            userArticlesService.MarkAsRead(userId, articleId);
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
        //// DEFECT: do not use JObject type in parameters. use some model. deserialisation perform before it pass to controller. code is unsafe
        public void RemoveFromFavorites(JObject jsonData)
        {
            int userId = int.Parse(jsonData["userId"].ToString());
            int articleId = int.Parse(jsonData["articleId"].ToString());

            userArticlesService.RemoveFromFavorites(userId, articleId);
        }

        [HttpPost]
        //// see message above
        // DEFECT: duplication. change to single method AddRemoveFavorites
        public void AddToFavorites(JObject jsonData)
        {
            int userId = int.Parse(jsonData["userId"].ToString());
            int articleId = int.Parse(jsonData["articleId"].ToString());

            userArticlesService.AddToFavorites(userId, articleId);
        }

        [HttpGet]
        public UsersArticle ReadLaterInfo(int userId, int articleIdInfo)
        {
            return userArticlesService.ReadLaterInfo(userId, articleIdInfo);
        }

        [HttpPost]
        public UsersArticle ToReadLater(int userId, int articleIdRl, bool add)
        {
            return userArticlesService.ToReadLater(userId, articleIdRl, add);
        }

        [HttpGet]
        public bool CheckIsFavorite(int userId, int articleId)
        {
            return userArticlesService.CheckIsFavorite(userId, articleId);
        }

        public IEnumerable<Article> GetReadLeaterArticles()
        {
            var userId = 5;
            return userArticlesService.GetReadLaterArticles(userId);
        }
    }
}