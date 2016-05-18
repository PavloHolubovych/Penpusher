using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IArticleService
    {
        Article AddArticle(Article article);
        IEnumerable<Article> Find(string value);
        bool CheckDoesExists(string title);
        IEnumerable<Article> GetArticlesFromProvider(int newsProviderId);
        IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<NewsProvider> newsProviders);
    }
}