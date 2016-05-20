using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class ProviderTrackingService : IProviderTrackingService
    {
        private readonly INewsProviderService newsProvidersService;

        private ProviderTrackingService(INewsProviderService newsProviderService)
        {
            this.newsProvidersService = newsProviderService;
        }

        public IEnumerable<XDocument> GetRssFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
            //return providers.Select(provider => GetRssFile(provider.Link)).Where(rssFile => IsRssUpdated(rssFile, provider)).ToList();
            var rssCollection = new List<XDocument>();
            foreach (NewsProvider provider in providers)
            {
                XDocument rssFile = GetRssFile(provider.Link);
                DateTime lastBuildDate = GetLastBuildDateForRss(rssFile);
                if (IsRssUpdated(lastBuildDate, provider))
                {
                    rssCollection.Add(rssFile);
                }
            }
            return rssCollection;
        }

        // TODO: Vadym, +add Field LastbuildDate for Entity NewsProvider in Db, Testing
        private bool IsRssUpdated(DateTime lastBuildDate, NewsProvider provider) //params should be (lastBuildDate, provider.LastbuildDate)
        {
            return true;
        }

        // TODO: Testing?
        private XDocument GetRssFile(string link)
        {
            XDocument rssFile = XDocument.Load(link);
            return rssFile;
        }

        // TODO: Max
        private DateTime GetLastBuildDateForRss(XDocument rssFile)
        {
            var someDate = "ufiou";
            ParseDateTimeFormat(someDate);
            return DateTime.Now;
        }

        // TODO: Max
        private DateTime ParseDateTimeFormat(string date)
        {
            return DateTime.Now;
        }
    }
}