// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsersArticlesService.cs" company="Sigma software">
//   UsersArticlesService Interface
// </copyright>
// <summary>
//   Defines the IUsersArticlesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// The UsersArticlesService interface.
    /// </summary>
    public interface IUsersArticlesService
    {
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
        IEnumerable<UsersArticle> GetUsersReadArticles(int userId);

        /// <summary>
        /// The mark as read.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        void MarkAsRead(int userId, int articleId);

        /// <summary>
        /// The add to favorites.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        void AddToFavorites(int userId, int articleId);

        /// <summary>
        /// The remove from favorites.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="articleId">
        /// The article id.
        /// </param>
        void RemoveFromFavorites(int userId, int articleId);

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
        bool CheckIsFavorite(int userId, int articleId);

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
        /// The add or delete item.
        /// </param>
        /// <returns>
        /// The <see cref="UsersArticle"/>.
        /// </returns>
        UsersArticle ToReadLater(int userId, int articleId, bool add);

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
        UsersArticle ReadLaterInfo(int userId, int articleId);

        IEnumerable<UsersArticle> GetReadLaterArticles(int userId);
    }
}