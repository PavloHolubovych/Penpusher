using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services
{
    public interface IArticleService
    {
        Article AddArticle(Article article);

        IEnumerable<Article> Find(string value);

        bool CheckDoesExists(string title);

        IEnumerable<Article> GetArticlesFromProvider(int newsProviderId);

        IEnumerable<Article> GetAllArticleses();

        IEnumerable<Article> GetAllUnreadArticles(IEnumerable<UserNewsProviderModels> newsProviders, int pageNumber);

        IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<UserNewsProviderModels> newsProviders);

        Article GetById(int id);
    }
}