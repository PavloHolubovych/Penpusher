using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public interface IParser
    {
        IEnumerable<Article> GetParsedArticles(RssChannelModel rssModel);
    }
}
