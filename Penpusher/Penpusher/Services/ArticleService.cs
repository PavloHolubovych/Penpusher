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
            return new Article().CloneClient(repository.GetById(id)); 
        }

        public bool CheckDoesExists(string link)
        {
            return repository.GetAll().Any(x => x.Link == link);
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
            List<int> idProviders = newsProviders.Select(d => d.IdNewsProvider).ToList();
            if (idProviders.Any())
            {
                return repository.GetAll().Where(x => idProviders.Contains(x.IdNewsProvider) && x.UsersArticles.Count == 0).Select(
                            o =>
                            new Article()
                            {
                                Id = o.Id,
                                Title = o.Title,
                                Description = o.Description,
                                Link = o.Link,
                                Date = o.Date,
                                Image = o.Image
                            });
            }
            return new List<Article>();
        }
    }
}