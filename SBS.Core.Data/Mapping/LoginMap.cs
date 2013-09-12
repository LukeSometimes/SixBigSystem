using SBS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace SBS.Core.Data
{
    public class LoginMap : EntityTypeConfiguration<Login>
    {
        public LoginMap()
            : base()
        {
           

        }
    }
}
