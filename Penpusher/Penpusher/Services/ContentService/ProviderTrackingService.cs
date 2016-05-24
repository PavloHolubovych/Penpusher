using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Penpusher.Models;

namespace Penpusher.Services.ContentService
{
    public class ProviderTrackingService : IProviderTrackingService
    {
        private readonly IDataBaseServiceExtension dbServiceExtension;
        private readonly INewsProviderService newsProvidersService;
        private readonly IRssReader rssReader;

        public ProviderTrackingService(
            IDataBaseServiceExtension dbServiceExtension,
            INewsProviderService newsProviderService,
            IRssReader rssReader)
        {
            this.dbServiceExtension = dbServiceExtension;
            this.newsProvidersService = newsProviderService;
            this.rssReader = rssReader;
        }

        // TODO: test
        public void UpdateArticlesFromNewsProviders()
        {
            List<RssChannelModel> updatedRssChannells = GetUpdatedRssFilesFromNewsProviders().ToList();
            if (updatedRssChannells.Any())
            {
                dbServiceExtension.InsertNewArticles(updatedRssChannells);
            }
        }

        // TODO: test with null date
        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
            var updatedRssChannells = new List<RssChannelModel>();
            foreach (NewsProvider provider in providers)
            {
                XDocument rssFile = rssReader.GetRssFileByLink(provider.Link);
                DateTime? lastBuildDateFromRss = GetLastBuildDateForRss(rssFile);
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

        // TODO: try/catch
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

        // TODO: try/catch
        private DateTime ParseDateTimeFormat(string date)
        {
            DateTime dateTime;
            DateTime.TryParse(date, out dateTime);
            return dateTime;
        }
    }
}