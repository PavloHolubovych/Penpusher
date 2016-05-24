using System;
using System.Collections.Generic;
using System.Linq;
using Penpusher.Models;
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
            return this.repository.Add(article);
        }

        public void AddArticle()
        {
            NewsProvider provider = new NewsProvider { Id = 2 };
            var article = new Article
            {
                Id = 177,
                Date = DateTime.Now,
                Description = "Ti mayesh vzletiti krihitko!",
                IdNewsProvider = provider.Id,
                Link = "Link to sky",
                Title = "From job"
            };
            AddArticle(article);
        }

        public Article GetById(int id)
        {
            return this.repository.GetAll().Where(article => article.Id == id).Select(n => new Article()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Link = n.Link,
                Date = n.Date,
            }).ToList()[0];
        }

        public bool CheckDoesExists(string title)
        {
            return this.repository.GetAll().Count(x => x.Title == title) > 0;
        }

        public override IEnumerable<Article> Find(string title)
        {
            return this.repository.GetAll().Where(x => x.Title == title).Select(n => new Article()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Link = n.Link,
                Date = n.Date,
            });
        }

        /// <summary>
        /// The get articles from provider.
        /// </summary>
        /// <param name="newsProviderId">
        /// The news provider id.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        /// </returns>
        public IEnumerable<Article> GetArticlesFromProvider(int newsProviderId)
        {
            // I changed this code for performance. Selected only necessary fields
            // var vari = _repository.GetAll().Where(x => x.IdNewsProvider == newsProviderId).ToList();
            return this.repository.GetAll().Where(x => x.IdNewsProvider == newsProviderId).Select(o => new Article { Title = o.Title, Description = o.Description, Link = o.Link }).ToList();
        }

        public IEnumerable<Article> GetAllArticleses()
        {
            return this.repository.GetAll().Select(n => new Article()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Link = n.Link,
                Date = n.Date,
            });
        }



        public IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            if (newsProviders.ToList().Count > 0)
            {
                foreach (UserNewsProviderModels provider in newsProviders)
                {
                    var nextProviderArticles = GetArticlesFromProvider(provider.IdNewsProvider).Select(n => new Article()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        Link = n.Link,
                        Date = n.Date,
                    }).ToList();
                    articles.AddRange(nextProviderArticles);
                }
            }
            return articles;
        }
    }
}
