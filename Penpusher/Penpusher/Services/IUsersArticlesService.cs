using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<UsersArticle> GetUsersReadArticles(int userId);

        void MarkAsRead(int userId, int articleId);

        void AddToReadLater(int userId, int articleId);
        void AddToFavorites(int userId, int articleId);

        void RemoveFromFavorites(int userId, int articleId);

        bool CheckIsFavorite(int userId, int articleId);

        UsersArticle ToReadLater(int userId, int articleId, bool add);
        UsersArticle ReadLaterInfo(int userId, int articleId);
    }
}
