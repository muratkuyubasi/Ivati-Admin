using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class UserDetailRepository : GenericRepository<UserDetail, PTContext>,
          IUserDetailRepository
    {
        public UserDetailRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
