using SBS.Core.Data.Repositories;
using SBS.Site.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBS.Site.Service
{
    public class AccountService : IAccountService
    {
        public bool Login(LoginModel model)
        {
            using (var unitWork = new SBSUnitOfWork())
            {
                //var repo = new AccountRepository(unitWork);
               
                //var userInfo = repo.Where(x => x.UserName == model.UserName);
                //foreach (var u in userInfo)
                //{
                //    if (u.Password == model.Password)
                //    {
                //        return true;
                //    }
                //}
                
            }
            return false;
        }
    }
}
