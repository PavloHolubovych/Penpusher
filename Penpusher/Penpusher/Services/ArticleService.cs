using System.Collections.Generic;
using System.Linq;
using Penpusher.DAL;
using Penpusher.Services.Base;

namespace Penpusher.Services
{
    public class ArticleService : ServiceBase<Article>, IArticleService
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
            return repository.GetAll<Article>().Count(x => x.Title == title) > 0;
        }

        public override IEnumerable<Article> Find(string title)
        {
            return repository.GetAll<Article>().Where(_ => _.Title == title);
        }
    }
}