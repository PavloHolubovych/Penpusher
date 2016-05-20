using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penpusher.Services.ContentService
{
    public interface IParser
    {
        List<Article> Parse(string rss);

        IEnumerable<string> GetFeed(string url);
    }
}
