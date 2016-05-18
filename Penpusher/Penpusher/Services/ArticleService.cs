using System.Collections.Generic;
using System.Linq;
using Penpusher.Services.Base;

namespace Penpusher.Services
{
    public class ArticleService : ServiceBase<Article>, IArticleService
    {
        private readonly IRepository<Article> _repository;

        public ArticleService(IRepository<Article> repository)
        {
            _repository = repository;
        }

        public Article AddArticle(Article article)
        {
            return _repository.Add(article);
        }

        public bool CheckDoesExists(string title)
        {
            return _repository.GetAll().Count(x => x.Title == title) > 0;
        }

        public override IEnumerable<Article> Find(string title)
        {
            return _repository.GetAll().Where(_ => _.Title == title);
        }

        public override IEnumerable<Article> GetArticlesFromProvider(int newsProviderId)
        {
            return _repository.GetAll().Where(_ => _.IdNewsProvider == newsProviderId).ToList();
        }

        public IEnumerable<Article> GetAllArticleses()
        {
            return _repository.GetAll();
        }
    }
}
