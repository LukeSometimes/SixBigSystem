using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SBS.Data.Common;
using SBS.Core.Entity;
namespace SBS.Core.Data.Repositories
{
    public class AccountRepository : BaseRepository<Login>, IAccountRepository
    {
        private SBSDataContext context;

        public AccountRepository(SBSUnitOfWork unitOfWork)
            : base(unitOfWork.Context)
        {
            this.context = unitOfWork.Context;
        }

        public Login GetLoginByUserName(string userName)
        {
            return null;
        }
    }

    public interface IAccountRepository : IRepository<Login>
    {

    }
}
