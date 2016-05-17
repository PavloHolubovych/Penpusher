using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Penpusher.DAL
{
    class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The entities context.
        /// </summary>
        private PenpusherDatabaseEntities EntitiesContext = new PenpusherDatabaseEntities();

        /// <summary>
        /// The db set.
        /// </summary>
        private DbSet<T> dbSet;

        public Repository()
        {
            this.dbSet = EntitiesContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            dbSet.Add(entity);
            EntitiesContext.SaveChanges();
            return entity;
        }

        public T Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var item = dbSet.Find(id);
            dbSet.Remove(item);
            EntitiesContext.SaveChanges();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}