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

        //i dont really like void type for methods changing db state
        public void UpdateArticlesFromNewsProviders()
        {
            List<RssChannelModel> updatedRssChannells = GetUpdatedRssFilesFromNewsProviders().ToList();
            if (updatedRssChannells.Any())
            {
                dbServiceExtension.InsertNewArticles(updatedRssChannells);
            }
        }

        public IEnumerable<RssChannelModel> GetUpdatedRssFilesFromNewsProviders()
        {
            IEnumerable<NewsProvider> providers = newsProvidersService.GetAll();
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
                //DEFECT: duplication detected. move to method
                string lastBuild = null;
                try
                {
                    lastBuild = (string)rootElement.Element("channel")
                    .Element("lastBuildDate");
                }
                catch
                {
                    // no root element channel
                    //DEFECT: please, do not use empty catch
                }
                if (lastBuild != null)
                {
                    return ParseDateTimeFormat(lastBuild);
                }
                string lastpubDate = null;
                try
                {
                    lastpubDate = (string)rootElement.Element("channel")
                    .Element("pubDate");
                }
                catch
                {
                    // no root element channel
                    //again empty catch
                }
                if (lastpubDate != null)
                {
                    return ParseDateTimeFormat(lastpubDate);
                }
                ////return null;
            }
            return null;
        }

        private DateTime? ParseDateTimeFormat(string date)
        {
            DateTime? newdate = null;
            try
            {
                string dt = DateTime.ParseExact(date, @"ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy HH:mm:ss");
                newdate = Convert.ToDateTime(dt);
            }
            catch
            {
                // if string date is unreadable
            }
            return newdate;
        }
    }
}