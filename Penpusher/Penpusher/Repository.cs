using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Penpusher
{
    class Repository<T> : IRepository<T> where T : class
    {
        private PenpusherEntities EntitiesContext = new PenpusherEntities();
        internal DbSet<T> dbSet;

        public Repository()
        {
            this.dbSet = EntitiesContext.Set<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public void Add<TEntity>(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public T1 Edit<T1>(T1 entity)
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