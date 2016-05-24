// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleService.cs" company="Sigma software">
//   Article service
// </copyright>
// <summary>
//   The article service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Penpusher.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Models;

    using Base;

    /// <summary>
    /// The article service.
    /// </summary>
    public class ArticleService : ServiceBase<Article>, IArticleService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<Article> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public ArticleService(IRepository<Article> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// The add article.
        /// </summary>
        /// <param name="article">
        /// The article.
        /// </param>
        /// <returns>
        /// The <see cref="Article"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public Article AddArticle(Article article)
        {
            return repository.Add(article);
        }

        /// <summary>
        /// The add article.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void AddArticle()
        {
            var provider = new NewsProvider { Id = 2 };
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

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Article"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
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

        /// <summary>
        /// The check does exists.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public bool CheckDoesExists(string title)
        {
            return repository.GetAll().Count(x => x.Title == title) > 0;
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override IEnumerable<Article> Find(string title)
        {
            return repository.GetAll().Where(x => x.Title == title).Select(n => new Article()
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
            return repository.GetAll().Where(x => x.IdNewsProvider == newsProviderId).Select(o => new Article { Title = o.Title, Description = o.Description, Link = o.Link }).ToList();
        }

        /// <summary>
        /// The get all articleses.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Article> GetAllArticleses()
        {
            return repository.GetAll().Select(n => new Article()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Link = n.Link,
                Date = n.Date,
            });
        }

        /// <summary>
        /// The get articles from selected providers.
        /// </summary>
        /// <param name="newsProviders">
        /// The news providers.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            IEnumerable<UserNewsProviderModels> userNewsProviderModelses = newsProviders as UserNewsProviderModels[] ?? newsProviders.ToArray();
            if (userNewsProviderModelses.ToList().Count > 0)
            {
                foreach (UserNewsProviderModels provider in userNewsProviderModelses)
                {
                    List<Article> nextProviderArticles = GetArticlesFromProvider(provider.IdNewsProvider).Select(n => new Article()
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