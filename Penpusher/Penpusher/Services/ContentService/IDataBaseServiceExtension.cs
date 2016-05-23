
using System.Collections.Generic;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public interface IDataBaseServiceExtension
    {
        void InserNewArticles(List<XDocument> providers, int idProvider);
        string GetRssUrlById(int id);
    }
}