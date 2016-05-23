using System;
using System.Collections.Generic;
using System.Globalization;
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
            string content = rssFile.ToString();
            XElement rootElement = XDocument.Parse(content).Root;
                if (rootElement != null)
                {
                    var lastBuild = (string)rootElement.Element("channel")
                        .Element("lastBuildDate");
                    if (lastBuild != null)
                    {
                        return ParseDateTimeFormat(lastBuild);
                    }

                var lastpubDate = (string)rootElement.Element("channel")
                            .Element("pubDate");
                if (lastpubDate != null)
                {
                    return ParseDateTimeFormat(lastpubDate);
                }
                    ////return null;
                }
            return null;
        }

        private DateTime ParseDateTimeFormat(string date)
        {
            string dt = DateTime.ParseExact(date, @"ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy HH:mm:ss");
            DateTime newdate = Convert.ToDateTime(dt);
            return newdate;
        }
    }
}