﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Penpusher.Models;
using Penpusher.Services;

namespace Penpusher.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly IArticleService articleService;
        private readonly IUsersArticlesService userArticlesService;
        private readonly INewsProviderService newsProviderService;

        public ArticlesController(
            IArticleService articleService,
            IUsersArticlesService userArticlesService,
            INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.userArticlesService = userArticlesService;
            this.newsProviderService = newsProviderService;
        }

        [HttpGet]
        [ActionName("ArticlesFromProvider")]
        public IEnumerable<Article> ArticlesFromProvider(int newsProviderId)
        {
            return articleService.GetArticlesFromProvider(newsProviderId);
        }

        [HttpPost]
        //// please do not use void. use bool instead
        public void MarkAsRead(int userId, int articleId)
        {
            userArticlesService.MarkAsRead(userId, articleId);
        }

        [HttpGet]
        public IEnumerable<Article> UserReadArticles(int userId)
        {
            return userArticlesService.GetUsersReadArticles(userId); 
        }

        [HttpGet]
        public IEnumerable<Article> UserFavoriteArticles(int userId)
        {
            return userArticlesService.GetUsersFavoriteArticles(userId);
        }

        [HttpGet]
        [ActionName("GetArticleDetail")]
        public Article ArticleDetails(int articleId)
        {
            return articleService.GetById(articleId);
        }

        [HttpGet]
        public IEnumerable<Article> ArticlesFromSelectedProviders(int userId)
        {
            IEnumerable<UserNewsProviderModels> newsProviders = newsProviderService.GetSubscriptionsByUserId(userId);
            return articleService.GetArticlesFromSelectedProviders(newsProviders);
        }

        [HttpPost]
       public void AddRemoveFavorites([FromBody]WebModel model)
        {
            userArticlesService.AddRemoveFavorites(model.UserId, model.ArticleId, model.Flag);
        }

        [HttpGet]
        public UsersArticle ReadLaterInfo(int userId, int articleIdInfo)
        {
            return userArticlesService.ReadLaterInfo(userId, articleIdInfo);
        }

        [HttpPost]
        public UsersArticle ToReadLater(int userId, int articleIdRl, bool add)
        {
            return userArticlesService.ToReadLater(userId, articleIdRl, add);
        }

        [HttpGet]
        public bool CheckIsFavorite(int userId, int articleId)
        {
            return userArticlesService.CheckIsFavorite(userId, articleId);
        }

        public IEnumerable<Article> GetReadLeaterArticles()
        {
            int userId = 5;
            return userArticlesService.GetReadLaterArticles(userId);
        }
    }
}