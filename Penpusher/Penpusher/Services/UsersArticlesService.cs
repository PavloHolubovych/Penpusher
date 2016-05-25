// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersArticlesService.cs" company="Sigma software">
//   Users Articles service
// </copyright>
// <summary>
//   Defines the UsersArticlesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// The users articles service.
    /// </summary>
    public class UsersArticlesService : IUsersArticlesService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<UsersArticle> repository;

        /// <summary>
        /// The article repository.
        /// </summary>
        private readonly IRepository<Article> articleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersArticlesService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="articleRepository">
        /// The article repository.
        /// </param>
        public UsersArticlesService(IRepository<UsersArticle> repository, IRepository<Article> articleRepository)
        {
            this.repository = repository;
            this.articleRepository = articleRepository;
        }

        /// <summary>
        /// The get users read articles.
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<UsersArticle> GetUsersReadArticles(int userId)
        {
            return repository.GetAll().Where(art => art.IsRead == true && art.UserId == userId).ToList();
        }

        public IEnumerable<UsersArticle> GetReadLaterArticles(int userId)
        {
            return repository.GetAll().Where(art => art.IsToReadLater == true && art.UserId == userId).ToList();
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public void MarkAsRead(int userId, int articleId)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = false,
                    IsFavorite = false,
                    IsRead = true
                };
            }
            else
            {
                if(!(bool)userArticle.IsToReadLater)
                userArticle.IsRead = true;
            }
            repository.Edit(userArticle);
        }

        /// <summary>
        /// The add to favorites.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public void AddToFavorites(int userId, int articleId)
        {
            UsersArticle userArticle =
                repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                                  {
                                      ArticleId = articleId,
                                      UserId = userId,
                                      IsToReadLater = false,
                                      IsFavorite = true,
                                      IsRead = true
                                  };
            }
            else
            {
                userArticle.IsFavorite = true;
            }

            repository.Edit(userArticle);
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public void RemoveFromFavorites(int userId, int articleId)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = false,
                    IsFavorite = false,
                    IsRead = true
                };
            }
            else
            {
                userArticle.IsFavorite = false;
            }

            repository.Edit(userArticle);
        }

        /// <summary>
        /// The read later info.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        /// <returns>
        /// The <see cref="UsersArticle"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:CurlyBracketsMustNotBeOmitted", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ReadLaterInfo(int userId, int articleId)
        {
            UsersArticle userArticle =
                repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if (userArticle != null)
            {
                var userArticleClient = new UsersArticle
                                            {
                                                Id = userArticle.Id,
                                                IsToReadLater = userArticle.IsToReadLater,
                                                IsRead = userArticle.IsRead,
                                                Article = null,
                                                User = null,
                                                ArticleId = userArticle.ArticleId,
                                                IsFavorite = userArticle.IsFavorite,
                                                UserId = userArticle.UserId
                                            };
                return userArticleClient;
            }

            return new UsersArticle();
        }

        /// <summary>
        /// The to read later.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        /// <param name="add">
        /// The add.
        /// </param>
        /// <returns>
        /// The <see cref="UsersArticle"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:CurlyBracketsMustNotBeOmitted", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ToReadLater(int userId, int articleId, bool add)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                                  {
                                      ArticleId = articleId,
                                      UserId = userId,
                                      IsToReadLater = add,
                                      IsFavorite = false,
                                      IsRead = false
                                  };
                repository.Add(userArticle);
            }
            else
            {
                userArticle.IsToReadLater = add;
                userArticle.IsRead = !add;
                repository.Edit(userArticle);
            }

            var userArticleClient = new UsersArticle
            {
                Id = userArticle.Id,
                IsToReadLater = userArticle.IsToReadLater,
                IsRead = userArticle.IsRead,
                Article = null,
                User = null,
                ArticleId = userArticle.ArticleId,
                IsFavorite = userArticle.IsFavorite,
                UserId = userArticle.UserId
            };
            return userArticleClient;
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed. Suppression is OK here.")]
        public bool CheckIsFavorite(int userId, int articleId)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            return userArticle != null && (bool)userArticle.IsFavorite;
        }
    }
}