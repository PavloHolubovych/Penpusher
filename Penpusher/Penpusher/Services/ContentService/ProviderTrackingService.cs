using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class ProviderTrackingService : IProviderTrackingService
    {
        private readonly IDataBaseServiceExtension dataBaseServiceExtension;
        private readonly INewsProviderService newsProviderService;
        private readonly IRssReader rssReader;

        public ProviderTrackingService(
            IDataBaseServiceExtension dataBaseServiceExtension,
            INewsProviderService newsProviderService,
            IRssReader rssReader)
        {
            this.dataBaseServiceExtension = dataBaseServiceExtension;
            this.newsProviderService = newsProviderService;
            this.rssReader = rssReader;
        }

        public void UpdateArticlesFromNewsProviders()
        {
            List<RssChannelModel> updatedRssChannells = GetUpdatedRssFilesFromNewsProviders().ToList();
            if (updatedRssChannells.Any())
            {
                dataBaseServiceExtension.InsertNewArticles(updatedRssChannells);
            }
        }

        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProviderService.GetAll();
            var updatedRssChannells = new List<RssChannelModel>();
            foreach (NewsProvider provider in providers)
            {
                DateTime? lastBuildDateFromRss = null;
                XDocument rssFile = rssReader.GetRssFileByLink(provider.Link);
                if (rssFile != null)
                {
                    lastBuildDateFromRss = GetLastBuildDateForRss(rssFile);
                }
                if (rssFile != null && IsRssUpdated(provider.LastBuildDate, lastBuildDateFromRss))
                {
                    var updatedChannel = new RssChannelModel { ProviderId = provider.Id, RssFile = rssFile, LastBuildDate = lastBuildDateFromRss };
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

        private DateTime? GetLastBuildDateForRss(XDocument rssFile)
        {
            string content = rssFile.ToString();
            XElement rootElement = XDocument.Parse(content).Root;
            if (rootElement != null)
            {
                string lastBuild = GetValueOfChildElementInLevel2(rootElement, "channel", "lastBuildDate");
                if (lastBuild != null)
                {
                    return ParseDateTimeFormat(lastBuild);
                }
                string lastpubDate = GetValueOfChildElementInLevel2(rootElement, "channel", "pubDate");
                if (lastpubDate != null)
                {
                    return ParseDateTimeFormat(lastpubDate);
                }
            }
            return null;
        }

        private string GetValueOfChildElementInLevel2(XElement rootElement, string childElementNameInLevel1, string childElementNameInLevel2)
        {
            XElement childElementInLevel1 = rootElement.Element(childElementNameInLevel1);
            if (childElementInLevel1 != null)
            {
                return (string)childElementInLevel1.Element(childElementNameInLevel2);
            }
            return null;
        }

        private DateTime? ParseDateTimeFormat(string date)
        {
            try
            {
                string dt = DateTime.ParseExact(date, @"ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy HH:mm:ss");
                return Convert.ToDateTime(dt);
            }
            catch
            {
                return null;
            }
        }
    }
}