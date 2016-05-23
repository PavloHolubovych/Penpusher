using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class ProviderTrackingService : IProviderTrackingService
    {
        private readonly INewsProviderService newsProvidersService;
        private readonly IRssReader rssReader;

        public ProviderTrackingService(INewsProviderService newsProviderService, IRssReader rssReader)
        {
            this.newsProvidersService = newsProviderService;
            this.rssReader = rssReader;
        }

        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
            var updatedRssChannells = new List<RssChannelModel>();
            foreach (NewsProvider provider in providers)
            {
                XDocument rssFile = rssReader.GetRssFileByLink(provider.Link);
                if (rssFile != null && IsRssUpdated(provider.LastBuildDate, GetLastBuildDateForRss(rssFile)))
                {
                    var updatedChannel = new RssChannelModel { ProviderId = provider.Id, RssFile = rssFile };
                    updatedRssChannells.Add(updatedChannel);
                }
            }
            return updatedRssChannells;
        }

        // TODO: Vadym, Testing
        private bool IsRssUpdated(DateTime? previousLastBuilDate, DateTime? updatedLastBuildDate)
        {
            if (updatedLastBuildDate == null || previousLastBuilDate == null)
            {
                return true;
            }
            return previousLastBuilDate < updatedLastBuildDate;
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