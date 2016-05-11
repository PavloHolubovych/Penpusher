using System.Collections.Generic;
using System.Linq;
using Penpusher.DAL;
using Penpusher.Services.Base;

namespace Penpusher.Services
{
    class ArticleService : ServiceBase<Article>, IArticleService
    {
        private readonly IRepository<Article> repository;

        public ArticleService(IRepository<Article> repository)
        {
            this.repository = repository;
        }

        public void AddArticle(Article article)
        {
            repository.Add(article);
        }

        public bool CheckDoesExists(string title)
        {
            return true;
        }

        public override IEnumerable<Article> Find(string title)
        {
            return repository.GetAll().Where(_ => _.Title == title);
        }
    }
}