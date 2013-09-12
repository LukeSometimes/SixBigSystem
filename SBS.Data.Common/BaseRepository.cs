using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Omu.ValueInjecter;

namespace SBS.Data.Common
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext dbContext;
        public BaseRepository(DbContext context)
        {
            dbContext = context;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public T Insert(T o)
        {
            var t = dbContext.Set<T>().Create();
            t.InjectFrom(o);
            dbContext.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T o)
        {
            dbContext.Set<T>().Remove(o);
        }

        public T Get(int id)
        {
            return dbContext.Set<T>().Find(id);
        }


        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

    }
}
