using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            //EntitiesContext.Configuration.ProxyCreationEnabled = false;
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
                                    
        public void Edit(T entity)
        {
            DbSet.AddOrUpdate(entity);
            EntitiesContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = DbSet.Find(id);
            DbSet.Remove(item);
            EntitiesContext.SaveChanges();
        }
        //TODO: implement this method and use it when we need to get entity by its id
        public T GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}