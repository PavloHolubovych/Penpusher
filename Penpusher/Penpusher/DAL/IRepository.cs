using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Extensions.Conventions.Syntax;

namespace Penpusher
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Edit<T>(T entity);
        void Delete(int id);
        T GetById(int id);
    }
}