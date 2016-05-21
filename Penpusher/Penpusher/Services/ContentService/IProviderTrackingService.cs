using System.Collections.Generic;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public interface IProviderTrackingService
    {
        IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders();
    }
}