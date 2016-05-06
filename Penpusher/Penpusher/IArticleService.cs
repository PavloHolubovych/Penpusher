using Penpusher;
using System;
using System.Web;

namespace Penpusher
{
    public interface IArticleService
    {
        void AddArticle(Article article);
        bool CheckDoesExists(string title);
    }
}