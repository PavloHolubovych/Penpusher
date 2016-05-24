using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<UsersArticle> GetUsersReadArticles(int userId);
        void MarkAsRead(int userId, int articleId);

        void AddToFavorites(int userId, int articleId);
    }
}
