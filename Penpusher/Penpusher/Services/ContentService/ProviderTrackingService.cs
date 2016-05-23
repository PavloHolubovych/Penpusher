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
        private readonly IRepository<NewsProvider> newsProviderRepository;


        public ProviderTrackingService(IRepository<NewsProvider> newsProviderRepository)
        {
            this.newsProviderRepository = newsProviderRepository;
        }

        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProviderRepository.GetAll();
            var updatedRssChannells = new List<RssChannelModel>();
            foreach (NewsProvider provider in providers)
            {
                XDocument rssFile = GetRssFileByLink(provider.Link);
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

        // TODO: Done
        private XDocument GetRssFileByLink(string link)
        {
            XDocument rssFile = null;
            try
            {
                rssFile = XDocument.Load(link);
            }
            catch
            {
                // ignored
            }
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