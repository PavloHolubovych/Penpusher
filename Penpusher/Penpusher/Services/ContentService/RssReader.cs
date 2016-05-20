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
        public IEnumerable<XElement> ReadFeed(string url)
        {
            var rssFeed = XDocument.Load(url);
            var posts = rssFeed.Descendants("item");

            return posts;
        }
    }
}
