using System.Collections.Generic;
using System.Linq;

namespace Penpusher.Services
{
    public class UsersArticlesService : IUsersArticlesService
    {
        private readonly IRepository<UsersArticle> repository;
        private readonly IRepository< Article> articleRepository;

        public UsersArticlesService(IRepository<UsersArticle> repository, IRepository<Article> articleRepository)
        {
            this.repository = repository;
            this.articleRepository= articleRepository;
        }

        public IEnumerable<UsersArticle> GetUsersReadArticles(int userId)
        {
            return repository.GetAll().Where(art => art.IsRead == true && art.UserId == userId).ToList();
        }

        public void MarkAsRead(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if(userArticle==null)
            userArticle = new UsersArticle
            {
                ArticleId = articleId,
                UserId    = userId,
                IsToReadLater = false,
                IsFavorite = false,
                IsRead = true
            };
            else
            {
                userArticle.IsRead = true;
            }
            repository.Edit(userArticle);
        }

        public void AddToFavorites(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if (userArticle == null)
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = false,
                    IsFavorite = false,
                    IsRead = true
                };
            else
            {
                userArticle.IsFavorite = true;
            }
            repository.Edit(userArticle);
        }

    }
}