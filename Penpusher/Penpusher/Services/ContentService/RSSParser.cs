using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class RssParser : IParser
    {
        public IEnumerable<Article> GetParsedArticles(RssChannelModel rssModel)
        {
            IEnumerable<XElement> rssArticles = rssModel.RssFile.Descendants("item");

            return rssArticles.Select(post => ParseArticle(post, rssModel.ProviderId)).ToList();
        }

        private Article ParseArticle(XElement post, int newsProviderId)
        {
            DateTime date;
            if (!DateTime.TryParse(GetDescedantValue(post, "pubDate"), out date))
            {
                date = DateTime.Now;
            }

            return new Article
            {
                Description = GetDescedantValue(post, "description"),
                Title = GetDescedantValue(post, "title"),
                Date = date,
                Link = GetDescedantValue(post, "link"),
                Id = 0,
                Image = GetImage(post),
                IdNewsProvider = newsProviderId
            };
        }

        private string GetDescedantValue(XElement post, string descedantName)
        {
            return post.Element(descedantName)?.Value;
        }

        private string GetImage(XElement post)
        {
            XElement image = post.Element("enclosure");
            if (image != null && image.HasAttributes)
            {
                foreach (XAttribute attribute in image.Attributes())
                {
                    string value = attribute.Value.ToLower();
                    if (value.StartsWith("http://") && (value.EndsWith(".jpg") || value.EndsWith(".png") || value.EndsWith(".gif")))
                    {
                        return value; // Add here the image link to some array
                    }
                }
            }
            return null;
        }
    }
}