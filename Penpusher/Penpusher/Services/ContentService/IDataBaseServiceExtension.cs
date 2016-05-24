
using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public interface IDataBaseServiceExtension
    {
        void InsertNewArticles(IEnumerable<RssChannelModel> rssChannels);
        string GetRssUrlById(int id);
    }
}