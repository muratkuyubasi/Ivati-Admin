using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole, PTContext>,
       IUserRoleRepository
    {
        public UserRoleRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
