using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public interface IParser
    {
        IEnumerable<Article> GetParsedArticles(RssChannelModel rssModel);
    }
}