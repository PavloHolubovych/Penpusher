using System.Collections.Generic;
using Penpusher.Models;

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

        public void InsertNewArticles(IEnumerable<RssChannelModel> rssChannels)
        {
            foreach (RssChannelModel provider in rssChannels)
            {
                newsProviderService.UpdateLastBuildDateForNewsProvider(provider.ProviderId, provider.LastBuildDate);
                IEnumerable<Article> parsedArticles = rssParser.GetParsedArticles(provider);
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
                return newsProviderService.GetById(id).Link;
        }
    }
}