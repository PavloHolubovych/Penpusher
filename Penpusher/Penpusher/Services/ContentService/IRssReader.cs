using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public interface IRssReader
    {
        XDocument GetRssFileByLink(string link);
    }
}