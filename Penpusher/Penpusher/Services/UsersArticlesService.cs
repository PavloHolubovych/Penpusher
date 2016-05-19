using System.Collections.Generic;
using System.Linq;

namespace Penpusher.Services
{
    public class UsersArticlesService : IUsersArticlesService
    {
        private readonly IRepository<UsersArticle> repository;

        public UsersArticlesService(IRepository<UsersArticle> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<UsersArticle> GetUsersReadArticles(int userId)
        {
            return repository.GetAll().Where(art => art.IsRead.Value == true && art.UserId == userId).ToList();
        }
    }
}