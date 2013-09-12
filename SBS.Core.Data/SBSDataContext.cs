using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SBS.Core.Entity;

namespace SBS.Core.Data
{
    public class SBSDataContext : DbContext
    {
        public DbSet<Login> Login { get; set; }

        static SBSDataContext()
        {
            Database.SetInitializer<SBSDataContext>(null);
        }
        public SBSDataContext() : base("name=DefaultConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations
              .Add(new LoginMap());
        }
    }
}
