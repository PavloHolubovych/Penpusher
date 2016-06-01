using System.Collections.Generic;
using System.Web.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService articleService;
        private readonly IUsersArticlesService userArticlesService;
        private readonly INewsProviderService newsProviderService;
        private const int UserId = 5;

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
        public UsersArticle MarkAsRead([FromBody] ArticleModel data)
        {
            return userArticlesService.MarkAsRead(data.ArticleId);
        }

        [HttpGet]
        public IEnumerable<Article> UserReadArticles()
        {
            return userArticlesService.GetUsersReadArticles();
        }

        [HttpGet]
        public IEnumerable<Article> UserFavoriteArticles()
        {
            return userArticlesService.GetUsersFavoriteArticles();
        }

        [HttpGet]
        [ActionName("GetArticleDetail")]
        public Article ArticleDetails(int articleId)
        {
            return articleService.GetById(articleId);
        }

        [HttpGet]
        public IEnumerable<ArticleDto> ArticlesFromSelectedProviders()
        {
            IEnumerable<UserNewsProviderModels> newsProviders = newsProviderService.GetSubscriptionsByUserId();
            return articleService.GetAllUnreadArticles(newsProviders, UserId);
        }

        [HttpPost]
       public bool? AddRemoveFavorites([FromBody] ArticleModel data)
        {
           return userArticlesService.AddRemoveFavorites(data.ArticleId, data.Flag).IsFavorite;
        }

        [HttpGet]
        public UsersArticle UserArticleInfo(int articleIdInfo)
        {
            return userArticlesService.UserArticleInfo(articleIdInfo);
        }

        [HttpPost]
        public UsersArticle ToReadLater([FromBody] ArticleModel data)
        {
            return userArticlesService.ToReadLater(data.ArticleId, data.Flag);
        }

        public IEnumerable<Article> GetReadLeaterArticles()
        {
            return userArticlesService.GetReadLaterArticles();
        }
    }
}