using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class RssParser : IParser
    {
        public IEnumerable<Article> GetParsedArticles(RssChannelModel rssModel)
        {
            List<Article> parsedArticles = new List<Article>();
            IEnumerable<XElement> rssArticles = rssModel.RssFile.Descendants("item");

            foreach (XElement post in rssArticles)
                parsedArticles.Add(ParseArticle(post, rssModel.ProviderId));

            return parsedArticles;
        }

        private Article ParseArticle(XElement post, int idNewsProvider)
        {
            DateTime date;
            if (!DateTime.TryParse((GetDescedantValue(post, "pubDate")), out date))
                date = DateTime.Now;

            return new Article()
            {
                Description = WebUtility.HtmlEncode(GetDescedantValue(post, "description")),
                Title = GetDescedantValue(post, "title"),
                Date = date,
                Link = GetDescedantValue(post, "link"),
                Id = 0,
                IdNewsProvider = idNewsProvider,
                UsersArticles = null,
                NewsProvider = null
            };
        }

        private string GetDescedantValue(XElement post, string descedantName)
        {
            return post.Element(descedantName)?.Value;
        }
    }
}
