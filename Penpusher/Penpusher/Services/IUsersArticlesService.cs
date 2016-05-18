using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penpusher.Services
{
    public interface IUsersArticlesService
    {
        IEnumerable<UsersArticle> GetUsersReadArticles(int userId);
    }
}
