using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ninject;
using NUnit.Framework;
using Pci.TestUtils;
using Penpusher.Models;
using Penpusher.Services.ContentService;

namespace Penpusher.Test.Services.ContentService
{
    [TestFixture]
    public class RssParserTest : TestBase
    {
        [SetUp]
        public override void Testinitialize()
        {
            base.Testinitialize();
            MockKernel.Bind<IParser>().To<RssParser>();
        }

        [Category("RssParser")]
        [TestCase(TestName = "Checks correct rss parsing")]
        public void GetParsedArticlesTest()
        {
            var rssModel = new RssChannelModel
            {
                RssFile = new XDocument(
                    new XElement(
                        "rss",
                        new XAttribute("version", "2.0"),
                        new XElement(
                            "chanel",
                            new XElement("title", "Title"),
                            new XElement("link", "Link")),
                        new XElement(
                            "item",
                            new XElement("title", "Test Title1"),
                            new XElement("link", "Test link1"),
                            new XElement("description", "Test Description"),
                            new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500")),
                        new XElement(
                            "item",
                            new XElement("title", "Test Title2"),
                            new XElement("link", "Test link2"),
                            new XElement("description", "Test Description"),
                            new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500")),
                        new XElement(
                            "item",
                            new XElement("title", "Test Title3"),
                            new XElement("link", "Test link3"),
                            new XElement("description", "Test Description"),
                            new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500")))),
                ProviderId = 1
            };

            var expected = new List<Article>
            {
                new Article
                {
                    Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description",
                    Link = "Test link1", Title = "Test Title1", Id = 0, IdNewsProvider = 1, UsersArticles = null
                },
                new Article
                {
                    Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description",
                    Link = "Test link2", Title = "Test Title2", Id = 0, IdNewsProvider = 1, UsersArticles = null
                },
                new Article
                {
                    Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description",
                    Link = "Test link3", Title = "Test Title3", Id = 0, IdNewsProvider = 1, UsersArticles = null
                }
            };

            IEnumerable<Article> actual = MockKernel.Get<IParser>().GetParsedArticles(rssModel);

            Assert.That(actual, Is.EquivalentTo(expected).Using(new PropertiesEqualityComparer<Article>()));
        }
    }
}