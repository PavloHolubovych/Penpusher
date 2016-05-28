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
            return repository.Add(article);
        }

        public void AddArticle()
        {
            var provider = new NewsProvider
            {
                Id = 2
            };
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
            Article getarticle = repository.GetById(id);
            if (getarticle == null)
            {
                return null;
            }
            var article = new Article
            {
                Id = getarticle.Id,
                Title = getarticle.Title,
                Description = getarticle.Description,
                Link = getarticle.Link,
                Date = getarticle.Date
            };
            return article;
        }

        public bool CheckDoesExists(string title)
        {
            return repository.GetAll().Any(x => x.Title == title);
        }

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

        public IEnumerable<Article> GetArticlesFromProvider(int newsProviderId)
        {
            return repository.GetAll().Where(x => x.IdNewsProvider == newsProviderId).Select(o => new Article()
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Link = o.Link,
                Date = o.Date,
                Image = o.Image
            })
                .ToList();
        }

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

        public IEnumerable<Article> GetAllUnreadArticles(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            IEnumerable<UserNewsProviderModels> userNewsProviderModelses = newsProviders as UserNewsProviderModels[] ??
                                                                           newsProviders.ToArray();
            if (userNewsProviderModelses.Any())
            {
                foreach (UserNewsProviderModels provider in userNewsProviderModelses)
                {
                    IEnumerable<Article> nextProviderArticles =
                        repository.GetAll().Where(x => x.IdNewsProvider == provider.IdNewsProvider);
                    IEnumerable<Article> art = nextProviderArticles.Where(j => j.UsersArticles.Count == 0).Select(j => new Article()
                    {
                        Id = j.Id,
                        Title = j.Title,
                        Image = j.Image,
                        Description = j.Description,
                        Link = j.Link,
                        Date = j.Date,
                    });
                    articles.AddRange(art);
                }
            }

            return articles;
        }

        public IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            IEnumerable<UserNewsProviderModels> userNewsProviderModelses = newsProviders as UserNewsProviderModels[] ?? newsProviders.ToArray();
            if (userNewsProviderModelses.Any())
            {
                foreach (UserNewsProviderModels provider in userNewsProviderModelses)
                {
                    List<Article> nextProviderArticles = GetArticlesFromProvider(provider.IdNewsProvider).Select(n => new Article()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        Image = n.Image,
                        Link = n.Link,
                        Date = n.Date
                    }).ToList();
                    articles.AddRange(nextProviderArticles);
                }
            }

            return articles;
        }
    }
}