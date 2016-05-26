using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<Article> GetUsersReadArticles(int userId);

        IEnumerable<Article> GetUsersFavoriteArticles(int userId);

        void MarkAsRead(int userId, int articleId);

        void AddRemoveFavorites(int userId, int articleId, bool favoriteFlag);

        bool CheckIsFavorite(int userId, int articleId);

        UsersArticle ToReadLater(int userId, int articleId, bool add);

        UsersArticle ReadLaterInfo(int userId, int articleId);

        IEnumerable<Article> GetReadLaterArticles(int userId);

        ////IEnumerable<UsersArticle> GetReadLaterArticles(int userId);
    }
}