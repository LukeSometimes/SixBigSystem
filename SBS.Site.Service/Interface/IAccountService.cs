using SBS.Site.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBS.Site.Service
{
    public interface IAccountService
    {
        bool Login(LoginModel model);
    }
}
