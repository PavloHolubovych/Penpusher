using System.Collections.Generic;
using System.Linq;

namespace Penpusher.Services
{
    public class UsersArticlesService : IUsersArticlesService
    {
        private readonly IRepository<UsersArticle> _userArticleRepository;

        public UsersArticlesService(IRepository<UsersArticle> repository)
        {
            _userArticleRepository = repository;
        }

        public IEnumerable<Article> GetUsersReadArticles()
        {
            IEnumerable<Article> readArticles = _userArticleRepository.GetAll().Where(art => art.IsRead == true && art.UserId == Constants.UserId).Select(a => new Article
            {
                Id = a.Article.Id,
                Title = a.Article.Title,
                Description = a.Article.Description,
                Image = a.Article.Image,
                Link = a.Article.Link,
            });
            return readArticles;
        }

        public IEnumerable<Article> GetReadLaterArticles()
        {
            IEnumerable<Article> readArticles = _userArticleRepository.GetAll().Where(art => art.IsToReadLater == true && art.UserId == Constants.UserId).Select(a => new Article
            {
                Id = a.Article.Id,
                Title = a.Article.Title,
                Description = a.Article.Description,
                Image = a.Article.Image,
                Link = a.Article.Link,
            });
            return readArticles;
        }

        public IEnumerable<Article> GetUsersFavoriteArticles()
        {
           IEnumerable<Article> usart = _userArticleRepository.GetAll().Where(art => art.IsFavorite == true && art.UserId == Constants.UserId).Select(a => new Article
           {
               Id = a.Article.Id,
               Title = a.Article.Title,
               Description = a.Article.Description,
               Image = a.Article.Image,
               Link = a.Article.Link,
           });
            return usart;
        }

        public UsersArticle MarkAsRead(int articleId)
        {
            UsersArticle userArticle = _userArticleRepository.GetAll()
                .FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == Constants.UserId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                {
                    ArticleId = articleId,
                    UserId = Constants.UserId,
                    IsToReadLater = false,
                    IsFavorite = false,
                    IsRead = true
                };
            }
            else
            {
                if (userArticle.IsToReadLater != null && userArticle.IsToReadLater.Value)
                {
                    userArticle.IsRead = true;
                    userArticle.IsToReadLater = false;
                }
            }
            return _userArticleRepository.Edit(userArticle);
        }

        public UsersArticle AddRemoveFavorites(int articleId, bool favoriteFlag)
        {
            UsersArticle userArticle = _userArticleRepository.GetAll()
                .FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == Constants.UserId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                                  {
                                      ArticleId = articleId,
                                      UserId = Constants.UserId,
                                      IsToReadLater = false,
                                      IsFavorite = favoriteFlag,
                                      IsRead = true
                                  };
            }
            else
            {
                userArticle.IsFavorite = favoriteFlag;
            }

            return _userArticleRepository.Edit(userArticle);
        }

        public UsersArticle UserArticleInfo(int articleId)
        {
            UsersArticle userArticle = _userArticleRepository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == Constants.UserId);

            if (userArticle != null)
            {
                var userArticleClient = new UsersArticle
                                            {
                                                Id = userArticle.Id,
                                                IsToReadLater = userArticle.IsToReadLater,
                                                IsRead = userArticle.IsRead,
                                                ArticleId = userArticle.ArticleId,
                                                IsFavorite = userArticle.IsFavorite,
                                                UserId = userArticle.UserId
                                            };
                return userArticleClient;
            }
            return null;
        }

        public UsersArticle ToReadLater(int articleId, bool add)
        {
            UsersArticle userArticle = _userArticleRepository.GetAll()
                .FirstOrDefault(x => x.ArticleId == articleId && x.UserId == Constants.UserId);

            if (userArticle == null)
            {
                userArticle = new UsersArticle
                                  {
                                      ArticleId = articleId,
                                      UserId = Constants.UserId,
                                      IsToReadLater = add,
                                      IsFavorite = false,
                                      IsRead = false
                                  };
                _userArticleRepository.Add(userArticle);
            }
            else
            {
                userArticle.IsToReadLater = add;
                userArticle.IsRead = !add;
                _userArticleRepository.Edit(userArticle);
            }

            var userArticleClient = new UsersArticle
            {
                Id = userArticle.Id,
                IsToReadLater = userArticle.IsToReadLater,
                IsRead = userArticle.IsRead,
                ArticleId = userArticle.ArticleId,
                IsFavorite = userArticle.IsFavorite,
                UserId = userArticle.UserId
            };
            return userArticleClient;
        }
    }
}