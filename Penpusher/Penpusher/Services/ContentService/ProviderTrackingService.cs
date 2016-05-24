﻿using System;
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
            if (updatedRssChannells.Count > 0)
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

        // TODO: try/catch
        private DateTime? GetLastBuildDateForRss(XDocument rssFile)
        {
            string content = rssFile.ToString();
            XElement rootElement = XDocument.Parse(content).Root;
                if (rootElement != null)
                {
                    string lastBuild = null;
                    try
                    {
                        lastBuild = (string)rootElement.Element("channel")
                        .Element("lastBuildDate");
                    }
                    catch
                    {
                        // no root element channel
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
                        }
                    if (lastpubDate != null)
                    {
                        return ParseDateTimeFormat(lastpubDate);
                    }
                        ////return null;
                }
            return null;
        }

        // TODO: try/catch
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