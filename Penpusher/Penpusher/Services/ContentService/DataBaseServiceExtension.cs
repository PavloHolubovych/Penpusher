using System.Collections.Generic;
using System.Linq; 

namespace Penpusher.Services.ContentService
{
    public class DataBaseServiceExtension
    {
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;
        public DataBaseServiceExtension(IArticleService articleService, INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
        }

        public void InserNewArticles(List<Article> articles)
        {
            foreach (Article article in articles)
            {
                if (!articleService.CheckDoesExists(article.Link))
                    articleService.AddArticle(article);
            }
        }

        public string GetRssUrlById(int id)
        {
                return newsProviderService.GetAll().FirstOrDefault(x => x.Id == id).Link;
        }
    }
}