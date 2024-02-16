using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Repository
{
    public class UserModelRepository : GenericRepository<UserModel, PTContext>, IUserModelRepository
    {
        public UserModelRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
