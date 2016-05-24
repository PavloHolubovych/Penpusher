using System;
using System.Xml.Linq;

namespace Penpusher.Models
{
    public class RssChannelModel
    {
        public int ProviderId { get; set; }

        public DateTime? LastBuildDate { get; set; }

        public XDocument RssFile { get; set; }
    }
}