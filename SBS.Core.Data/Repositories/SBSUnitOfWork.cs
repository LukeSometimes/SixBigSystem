using SBS.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBS.Core.Data.Repositories
{
    public class SBSUnitOfWork : IUnitOfWork<SBSDataContext>
    {
        private readonly SBSDataContext context;

        public SBSUnitOfWork()
        {
            this.context = new SBSDataContext();
        }

        public SBSUnitOfWork(SBSDataContext context)
        {
            this.context = context;
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public SBSDataContext Context
        {
            get { return this.context; }
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
