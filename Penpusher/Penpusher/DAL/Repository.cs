using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Ninject.Infrastructure.Language;

namespace Penpusher.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> DbSet;
        private PenpusherDatabaseEntities EntitiesContext = new PenpusherDatabaseEntities();

        public Repository()
        {
            DbSet = EntitiesContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToEnumerable();
        }

        public T Add(T entity)
        {
            DbSet.Add(entity);
            EntitiesContext.SaveChanges();
            return entity;
        }

        public T Edit(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            DbSet.AddOrUpdate(entity);
            EntitiesContext.SaveChanges();
            return entity;
        }

        public T Delete(int id)
        {
            T item = DbSet.Find(id);
            if (item == null)
            {
                return null;
            }
            DbSet.Remove(item);
            EntitiesContext.SaveChanges();
            return item;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}