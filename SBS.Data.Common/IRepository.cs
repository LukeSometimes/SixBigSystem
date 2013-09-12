using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SBS.Data.Common
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        T Insert(T o);
        void Save();
        void Delete(T o);
      
    }
}
