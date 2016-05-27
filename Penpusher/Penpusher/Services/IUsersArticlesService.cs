using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<Article> GetUsersReadArticles();

        IEnumerable<Article> GetUsersFavoriteArticles();

        UsersArticle MarkAsRead(int articleId);

        UsersArticle AddRemoveFavorites(int articleId, bool favoriteFlag);

        bool CheckIsFavorite(int articleId);

        UsersArticle ToReadLater(int articleId, bool add);

        UsersArticle ReadLaterInfo(int articleId);

        IEnumerable<Article> GetReadLaterArticles();
    }
}