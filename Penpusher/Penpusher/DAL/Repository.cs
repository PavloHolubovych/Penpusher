using System;
using System.Collections.Generic;
using System.Data.Entity;
using Ninject.Infrastructure.Language;

namespace Penpusher.DAL
{
    class Repository<T> : IRepository<T> where T : class
    {
        private PenpusherDatabaseEntities EntitiesContext = new PenpusherDatabaseEntities();
        internal DbSet<T> DbSet;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var item = DbSet.Find(id);
            DbSet.Remove(item);
            EntitiesContext.SaveChanges();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}