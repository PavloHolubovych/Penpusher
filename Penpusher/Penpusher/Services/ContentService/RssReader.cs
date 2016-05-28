using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class RssReader : IRssReader
    {
        public XDocument GetRssFileByLink(string link)
        {
            try
            {
                return XDocument.Load(link);
            }
            catch
            {
                return null;
            }
        }
    }
}