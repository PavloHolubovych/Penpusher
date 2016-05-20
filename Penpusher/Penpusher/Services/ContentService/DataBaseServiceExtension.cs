using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class DataBaseServiceExtension
    {
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;
        private RSSParser rssParser;
        public DataBaseServiceExtension(IArticleService articleService, INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
            this.rssParser=new RSSParser();
        }

        public void InserNewArticles(List<XDocument> providers)
        {
            foreach (var provider in providers)
            {
                var parsedArticles = rssParser.GetParsedArticles(provider);
                foreach (var article in parsedArticles)
                {
                    if (!articleService.CheckDoesExists(article.Link))
                        articleService.AddArticle(article);
                }
               
            }
        }

        public string GetRssUrlById(int id)
        {
                return newsProviderService.GetAll().FirstOrDefault(x => x.Id == id).Link;
        }
    }
}