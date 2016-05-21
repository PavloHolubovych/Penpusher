using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class ProviderTrackingService : IProviderTrackingService
    {
        private readonly INewsProviderService newsProvidersService;

        public ProviderTrackingService(INewsProviderService newsProviderService)
        {
            this.newsProvidersService = newsProviderService;
        }

        public IEnumerable<XDocument> GetRssFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
            var rssCollection = new List<XDocument>();
            foreach (NewsProvider provider in providers)
            {
                XDocument rssFile = GetRssFile(provider.Link);
                if (IsRssUpdated(provider.LastBuildDate, GetLastBuildDateForRss(rssFile)))
                {
                    rssCollection.Add(rssFile);
                }
            }
            return rssCollection;
        }

        // TODO: Vadym, +Testing
        private bool IsRssUpdated(DateTime? previousLastBuilDate, DateTime? updatedLastBuildDate)
        {
            if (updatedLastBuildDate == null || previousLastBuilDate == null)
            {
                return true;
            }
            return previousLastBuilDate < updatedLastBuildDate;
        }

        // TODO: Testing?
        private XDocument GetRssFile(string link)
        {
            XDocument rssFile = XDocument.Load(link);
            return rssFile;
        }

        // TODO: Max, rss1.0 (rdf) - has no tag LastBuildDate, rss2.0 = ok: for rss1.0 use null
        private DateTime? GetLastBuildDateForRss(XDocument rssFile)
        {
            var someDate = "date"; 
            
            //var someDate = rssParser.GetLastBuildDate(rssFile);
            //if (someDate == null)
            //{
            //    return null;
            //}

            return ParseDateTimeFormat(someDate);
        }

        // TODO: Max
        private DateTime ParseDateTimeFormat(string date)
        {
            return DateTime.Now;
        }
    }
}