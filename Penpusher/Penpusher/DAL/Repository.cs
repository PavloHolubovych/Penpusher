using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Penpusher.DAL
{
    class Repository<T> : IRepository<T> where T : class
    {
        private PenpusherEntities EntitiesContext = new PenpusherEntities();
        internal DbSet<T> dbSet;

        public Repository()
        {
            this.dbSet = EntitiesContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            EntitiesContext.SaveChanges();
        }

        public T Edit (T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}