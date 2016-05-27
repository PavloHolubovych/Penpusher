using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<Article> GetUsersReadArticles(int userId);

        IEnumerable<Article> GetUsersFavoriteArticles(int userId);

        void MarkAsRead(int articleId);

        void AddRemoveFavorites(  int articleId, bool favoriteFlag);

        bool CheckIsFavorite( int articleId);

        UsersArticle ToReadLater( int articleId, bool add);

        UsersArticle ReadLaterInfo( int articleId);

        IEnumerable<Article> GetReadLaterArticles(int userId);
         
    }
}