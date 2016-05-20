using System.Collections.Generic;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public interface IProviderTrackingService
    {
        IEnumerable<XDocument> GetRssFromNewsProviders();
    }
}