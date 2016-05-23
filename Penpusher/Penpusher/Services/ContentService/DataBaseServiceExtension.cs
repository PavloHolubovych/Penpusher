using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class DataBaseServiceExtension
    {
        private readonly IArticleService articleService;
        private readonly INewsProviderService newsProviderService;
        private RssParser rssParser;
        public DataBaseServiceExtension(IArticleService articleService, INewsProviderService newsProviderService)
        {
            this.articleService = articleService;
            this.newsProviderService = newsProviderService;
            this.rssParser=new RssParser();
        }

        public void InsertNewArticles(IEnumerable<RssChannelModel> rssChannels)
        {
            foreach (RssChannelModel provider in rssChannels)
            {
                var parsedArticles = rssParser.GetParsedArticles(provider);
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