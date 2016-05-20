using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class RssReader
    {
        public IEnumerable<string> ReadFeed(string url)
        {
            var rssFeed = XDocument.Load(url);
            Console.Write(rssFeed);
            var posts = from item in rssFeed.Descendants("item")
                select item.Element("link").Value;

            return posts;
        }
    }
}
