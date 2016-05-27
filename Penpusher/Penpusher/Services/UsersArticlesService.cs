﻿using System.Collections.Generic;
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

        public IEnumerable<Article> GetUsersReadArticles()
        {
            IEnumerable<Article> readArticles = repository.GetAll().Where(art => art.IsRead == true && art.UserId == Constants.UserId).Select(a => new Article
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
            IEnumerable<Article> readArticles = repository.GetAll().Where(art => art.IsToReadLater.HasValue && art.UserId == Constants.UserId).Select(a => new Article
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
           IEnumerable<Article> usart = repository.GetAll().Where(art => art.IsFavorite == true && art.UserId == Constants.UserId).Select(a => new Article
           {
               Id = a.Article.Id,
               Title = a.Article.Title,
               Description = a.Article.Description,
               Image = a.Article.Image,
               Link = a.Article.Link,
           });
            return usart;
        }

        public void MarkAsRead(int articleId)
        {
            UsersArticle userArticle = repository.GetAll().
                FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == Constants.UserId);

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
            repository.Edit(userArticle);
        }


        public void AddRemoveFavorites( int articleId, bool favoriteFlag)
        {
            UsersArticle userArticle =
                repository.GetAll()
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

            repository.Edit(userArticle);
        }

        //DEFECT: naming
        public UsersArticle ReadLaterInfo(int articleId)
        {
            UsersArticle userArticle =
                repository.GetAll().FirstOrDefault(x => x.ArticleId == articleId && x.UserId == Constants.UserId);

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

        public UsersArticle ToReadLater( int articleId, bool add)
        {
            UsersArticle userArticle = repository.GetAll()
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
                repository.Add(userArticle);
            }
            else
            {
                userArticle.IsToReadLater = add;
                userArticle.IsRead = !add;
                repository.Edit(userArticle);
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

        //whats the purpose of this method? do we need it?
        public bool CheckIsFavorite( int articleId)
        {
            UsersArticle userArticle = repository.GetAll().FirstOrDefault(ua => ua.ArticleId == articleId && ua.UserId == Constants.UserId);

            return userArticle != null && (bool)userArticle.IsFavorite;//use C# 6 feature: e.g. return userArticle?.IsFavorite;
        }
    }
}