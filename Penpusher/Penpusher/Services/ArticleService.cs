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

        public Article AddArticle(Article article)
        {
            return repository.Add(article);
        }

        public bool CheckDoesExists(string title)
        {
            return repository.GetAll().Count(x => x.Title == title) > 0;
        }

        public override IEnumerable<Article> Find(string title)
        {
            return repository.GetAll().Where(_ => _.Title == title);
        }
        //parametr naming. id is not identifier in the current context. bettenr name is newsProviderId
        public override IEnumerable<Article> GetArticlesFromProvider(int id)
        {
            return repository.GetAll().Where(_ => _.IdNewsProvider == id).ToList();
        }
    }
}
