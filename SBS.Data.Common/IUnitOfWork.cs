using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SBS.Data.Common
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        int Save();
        TContext Context { get; }
    }
}
