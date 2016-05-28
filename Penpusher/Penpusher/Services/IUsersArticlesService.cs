using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<Article> GetUsersReadArticles();

        IEnumerable<Article> GetUsersFavoriteArticles();

        UsersArticle MarkAsRead(int articleId);

        UsersArticle AddRemoveFavorites(int articleId, bool favoriteFlag);

        UsersArticle ToReadLater(int articleId, bool add);

        UsersArticle UserArticleInfo(int articleId);

        IEnumerable<Article> GetReadLaterArticles();
    }
}