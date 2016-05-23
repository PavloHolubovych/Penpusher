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

        public ProviderTrackingService(INewsProviderService newsProviderService)
        {
            newsProvidersService = newsProviderService;
        }

        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
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

        private bool IsRssUpdated(DateTime? previousLastBuilDate, DateTime? updatedLastBuildDate)
        {
            if (updatedLastBuildDate == null || previousLastBuilDate == null)
            {
                return true;
            }
            return previousLastBuilDate < updatedLastBuildDate;
        }

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