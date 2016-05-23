using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace Penpusher.Services.ContentService
{
    public class DataBaseServiceExtension : IDataBaseServiceExtension
    {
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;
        private readonly IParser rssParser;

        public DataBaseServiceExtension(IArticleService articleService, INewsProviderService newsProviderService, IParser rssParser)
        {
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
            this.rssParser = rssParser;
        }

        /// <summary>
        /// The inser new articles.
        /// </summary>
        /// <param name="providers">
        /// The providers.
        /// </param>
        /// <param name="idProvider"></param>
        public void InserNewArticles(List<XDocument> providers, int idProvider)
        {
            foreach (XDocument provider in providers)
            {
                List<Article> parsedArticles = rssParser.GetParsedArticles(provider);
                foreach (Article article in parsedArticles)
                {
                    if (!articleService.CheckDoesExists(article.Link))
                    { 
                        articleService.AddArticle(article);
                    }
                }
            }
        }

        public string GetRssUrlById(int id)
        {
            return newsProviderService.GetAll().FirstOrDefault(x => x.Id == id).Link;
        }
    }
}