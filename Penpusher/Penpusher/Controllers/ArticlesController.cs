// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticlesController.cs" company="Sigma sowtware">
//   ArticlesController
// </copyright>
// <summary>
//   Defines the ArticlesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Http;

    using Models;

    using Services;

    /// <summary>
    /// The articles controller.
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2000:CodeLineMustNotEndWithWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    public class ArticlesController : ApiController
    {
        /// <summary>
        /// The _article service.
        /// </summary>
        private readonly IArticleService articleService;

        /// <summary>
        /// The _user articles service.
        /// </summary>
        private readonly IUsersArticlesService userArticlesService;

        /// <summary>
        /// The _news provider service.
        /// </summary>
        private readonly INewsProviderService newsProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="articleService">
        /// The article service.
        /// </param>
        /// <param name="userArticlesService">
        /// The user articles service.
        /// </param>
        /// <param name="newsProviderService">
        /// The news provider service.
        /// </param>
        public ArticlesController(
            IArticleService articleService,
            IUsersArticlesService userArticlesService,
            INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.userArticlesService = userArticlesService;
            this.newsProviderService = newsProviderService;
        }

        /// <summary>
        /// Get all articles by provider ID.
        /// </summary>
        /// <param name="newsProviderId">
        /// Provider ID
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return articleService.GetArticlesFromProvider(newsProviderId);
        }

        /// <summary>
        /// The mark as read.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        [HttpGet]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void MarkAsRead(int userId, int articleId)
        {
            userArticlesService.MarkAsRead(userId, articleId);
        }

        /// <summary>
        /// The user read articles.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [HttpGet]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Article> UserReadArticles(int userId)
        {
            IEnumerable<Article> readArticles = articleService.GetAllArticleses()
                .Join(
                    userArticlesService.GetUsersReadArticles(userId),
                    article => article.Id,
                    readArticle => readArticle.ArticleId,
                    (article, readArticle) => article);

            return readArticles;
        }

        /// <summary>
        /// The article details.
        /// </summary>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        /// <returns>
        /// The <see cref="Article"/>.
        /// </returns>
        [HttpGet]
        [ActionName("GetArticleDetail")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public Article ArticleDetails(int articleId)
        {
            return articleService.GetById(articleId);
        }

        /// <summary>
        /// The articles from selected providers.
        /// </summary>
        /// <param name="someUserId">
        /// The some user id.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [HttpGet]
        [ActionName("ArticlesFromSelectedProviders")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Article> ArticlesFromSelectedProviders(int someUserId)
        {
            IEnumerable<UserNewsProviderModels> newsProviders = newsProviderService.GetByUserId(someUserId);
            return articleService.GetArticlesFromSelectedProviders(newsProviders);
        }

        [HttpPost]
        public void RemoveFromFavorites(JObject jsonData)
        {
            int userId = int.Parse(jsonData["userId"].ToString());
            int articleId = int.Parse(jsonData["articleId"].ToString());

            _userArticlesService.RemoveFromFavorites(userId, articleId);
        }

        [HttpPost] 
        public void AddToFavorites([FromBody]JObject jsonData)
        /// <summary>
        /// The add to favorites.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        [HttpPost]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void AddToFavorites(int userId, int articleId)
        {
            int userId = int.Parse(jsonData["userId"].ToString());
            int articleId = int.Parse(jsonData["articleId"].ToString());

            _userArticlesService.AddToFavorites(userId, articleId);
        }

        /// <summary>
        /// The read later info.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleIdInfo">
        /// The article id info.
        /// </param>
        /// <returns>
        /// The <see cref="UsersArticle"/>.
        /// </returns>
        [HttpGet]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ReadLaterInfo(int userId, int articleIdInfo)
        {
            return userArticlesService.ReadLaterInfo(userId, articleIdInfo);
        }

        /// <summary>
        /// The to read later.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleIdRl">
        /// The article id rl.
        /// </param>
        /// <param name="add">
        /// The add.
        /// </param>
        /// <returns>
        /// The <see cref="UsersArticle"/>.
        /// </returns>
        [HttpPost]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ToReadLater(int userId, int articleIdRl, bool add)
        {
            return userArticlesService.ToReadLater(userId, articleIdRl, add);
        }

        /// <summary>
        /// The remove from favorites.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        [HttpPost]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void RemoveFromFavorites(int userId, int articleId)
        {
            userArticlesService.RemoveFromFavorites(userId, articleId);
        }

        /// <summary>
        /// The check is favorite.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [HttpGet]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public bool CheckIsFavorite(int userId, int articleId)
        {
            return userArticlesService.CheckIsFavorite(userId, articleId);
        }
    }
}