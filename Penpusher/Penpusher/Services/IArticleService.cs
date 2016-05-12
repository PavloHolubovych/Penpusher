using System.Collections.Generic;
using System.Linq;
using Penpusher.DAL;
using Penpusher.Services.Base;

namespace Penpusher.Services
{
    public interface IArticleService
    {
        Article AddArticle(Article article);
        IEnumerable<Article> Find(string value);
    }
}