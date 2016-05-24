using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Penpusher.Services
{
    public class UsersArticlesService : IUsersArticlesService
    {
        private readonly IRepository<UsersArticle> repository;
        private readonly IRepository<Article> articleRepository;

        public UsersArticlesService(IRepository<UsersArticle> repository, IRepository<Article> articleRepository)
        {
            this.repository = repository;
            this.articleRepository = articleRepository;
        }

        public IEnumerable<UsersArticle> GetUsersReadArticles(int userId)
        {
            return repository.GetAll().Where(art => art.IsRead == true && art.UserId == userId).ToList();
        }

        public void MarkAsRead(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

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

        public void AddToReadLater(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = true,
                    IsFavorite = false,
                    IsRead = false
                };
            }
            else
            {
                userArticle.IsFavorite = true;
                userArticle.IsRead = false;
            }
            repository.Edit(userArticle);
        }

        public void AddToFavorites(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            if (userArticle == null)
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = false,
                    IsFavorite = true,
                    IsRead = true
                };
            else
            {
                userArticle.IsFavorite = true;
            }
            repository.Edit(userArticle);
        }

        public void RemoveFromFavorites(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

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

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:CurlyBracketsMustNotBeOmitted", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ReadLaterInfo(int userId, int articleId)
        {
            UsersArticle userArticle =
                repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

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

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1503:CurlyBracketsMustNotBeOmitted", Justification = "Reviewed. Suppression is OK here.")]
        public UsersArticle ToReadLater(int userId, int articleId, bool add)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if (userArticle == null)
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IsToReadLater = add,
                    IsFavorite = false,
                    IsRead = false
                };
            else
            {
                userArticle.IsToReadLater = add;
                userArticle.IsRead = !add;
            }
            repository.Edit(userArticle);

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

        public bool CheckIsFavorite(int userId, int articleId)
        {
            var userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == userId);

            return userArticle != null && (bool)userArticle.IsFavorite;
        }
    }
}