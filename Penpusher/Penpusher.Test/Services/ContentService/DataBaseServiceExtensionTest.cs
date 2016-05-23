using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Penpusher.Services;
using Penpusher.Services.ContentService;

namespace Penpusher.Test.Services.ContentService
{
    [TestFixture]
    public class DataBaseServiceExtensionTest : TestBase
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
           
            MockKernel.Bind<INewsProviderService>().To<NewsProviderService>();
            MockKernel.Bind<IDataBaseServiceExtension>().To<DataBaseServiceExtension>();
        }

        [TestCase(TestName = "For existing user, that has read articles")]
        public void InsertNewArticle()
        {
            var rssDocument = new List<XDocument>()
            {
                new  XDocument(
                    new XElement("rss", new XAttribute("version", "2.0"),
                    new XElement("chanel",
                        new XElement("title", "Title"),
                        new XElement("link", "Link")),
                    new XElement("item",
                        new XElement("title", "Test Title1"),
                        new XElement("link", "Test link1"),
                        new XElement("description", "Test Description"),
                        new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500")),
                    new XElement("item",
                        new XElement("title", "Test Title2"),
                        new XElement("link", "Test link2"),
                        new XElement("description", "Test Description"),
                        new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500")),
                    new XElement("item",
                        new XElement("title", "Test Title3"),
                        new XElement("link", "Test link3"),
                        new XElement("description", "Test Description"),
                        new XElement("pubDate", "Mon, 23 May 2016 00:00:00 -0500"))))
                    };

            var expected = new List<Article>()
            {
                new Article() { Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description", Link = "Test link1", Title = "Test Title1" },
                new Article() { Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description", Link = "Test link2", Title = "Test Title2" },
                new Article() { Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"), Description = "Test Description", Link = "Test link3", Title = "Test Title3" }
            };
            var repository = new List<Article>()
            {
                new Article() { Title = "title1"}
            };
            MockKernel.GetMock<IParser>()
                .Setup(parser => parser.GetParsedArticles(It.IsAny<XDocument>()))
                .Returns((XDocument doc) =>
                {
                   var testList= new List<Article>()
                    {
                        new Article()
                        {
                            Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"),
                            Description = "Test Description",
                            Link = "Test link1",
                            Title = "Test Title1"
                        },
                        new Article()
                        {
                            Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"),
                            Description = "Test Description",
                            Link = "Test link2",
                            Title = "Test Title2"
                        },
                        new Article()
                        {
                            Date = DateTime.Parse("Mon, 23 May 2016 00:00:00 -0500"),
                            Description = "Test Description",
                            Link = "Test link3",
                            Title = "Test Title3"
                        }
                    };
                    return testList;
                });
            MockKernel.GetMock<IArticleService>()
                .Setup(artServ => artServ.CheckDoesExists(It.IsAny<string>()))
                .Returns<string>((title) => repository.Contains(new Article() { Title = title }));
            MockKernel.GetMock<IArticleService>()
                .Setup(artServ => artServ.AddArticle(It.IsAny<Article>()))
                .Callback((Article article) => { repository.Add(article); });
            MockKernel.Get<IDataBaseServiceExtension>().InserNewArticles(rssDocument, 1);
            Assert.AreEqual(repository[repository.Count - 1].Title, expected[expected.Count - 1].Title);
        }
    }
}

