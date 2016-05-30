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

        public Article GetById(int id)
        {
            return repository.GetById(id);
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
            }).ToList();
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

        public IEnumerable<Article> GetArticlesFromSelectedProviders(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            foreach (int providerId in newsProviders.Select(p => p.IdNewsProvider))
            {
                articles.AddRange(GetArticlesFromProvider(providerId));
            }

            return articles;
        }

        public IEnumerable<Article> GetAllUnreadArticles(IEnumerable<UserNewsProviderModels> newsProviders)
        {
            var articles = new List<Article>();

            IEnumerable<UserNewsProviderModels> userNewsProviderModelses = newsProviders as UserNewsProviderModels[] ?? newsProviders.ToArray();
            if (userNewsProviderModelses.Any())
            {
                foreach (UserNewsProviderModels provider in userNewsProviderModelses)
                {
                    IEnumerable<Article> nextProviderArticles = repository.GetAll().Where(x => x.IdNewsProvider == provider.IdNewsProvider);
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
    }
}