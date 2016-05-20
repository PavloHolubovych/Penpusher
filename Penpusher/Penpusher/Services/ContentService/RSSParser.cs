using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class RSSParser: IParser
    {
        public List<Article> Parse(string rss)
        {
            var posts = GetFeed(rss);
            foreach (var item in posts)
            {
                Console.WriteLine(item);
            }

            return new List<Article> {};
        }

        public IEnumerable<string> GetFeed(string url)
        {
            return new RssReader().ReadFeed(@"http://feeds.feedburner.com/city-adm-lviv-ua?format=xml");
        }

    }
}
 