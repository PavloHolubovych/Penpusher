using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class RSSParser : IParser
    {
        public List<Article> GetParsedArticles(XDocument rssDocument)
        {
            List<Article> parsedArticles = new List<Article>();
            IEnumerable<XElement> rssArticles = rssDocument.Descendants("item");
            foreach (XElement post in rssArticles)
                parsedArticles.Add(ParseArticle(post));
            return parsedArticles;
        }

        private Article ParseArticle(XElement post)
        {
            DateTime date;
            if(!DateTime.TryParse((GetDescedantValue(post, "pubDate")), out date))
                date = DateTime.Now;

            return new Article()
            {
                Description = GetDescedantValue(post, "description"),
                Title = GetDescedantValue(post, "title"),
                Date = date,
                Link = GetDescedantValue(post, "link")
            };
        }

        private string GetDescedantValue(XElement post, string descedantName)
        {
            return post.Element(descedantName).Value;
        }
    }
}
