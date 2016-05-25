using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Penpusher.Services.ContentService
{
    public class RssReader : IRssReader
    {
        // TODO: Testing
        public XDocument GetRssFileByLink(string link)
        {
            XDocument rssFile = null;
            try
            {
                rssFile = XDocument.Load(link);
            }
            catch
            {
                //DEFECT: bad practice to use empty catch
                // ignored
            }
            return rssFile;
        }
    }
}
