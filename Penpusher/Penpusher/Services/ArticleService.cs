using System.Collections.Generic;
using System.Linq;
using Penpusher.DAL;
using Penpusher.Services.Base;

namespace Penpusher.Services
{
    /// <summary>
    /// The article service.
    /// </summary>
    public class ArticleService : IArticleService
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

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Article> Find(string title)
        {
            return repository.GetAll<Article>().Where(_ => _.Title == title);
        }
    }
}