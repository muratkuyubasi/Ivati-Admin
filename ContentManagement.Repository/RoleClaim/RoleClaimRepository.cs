using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class RoleClaimRepository : GenericRepository<RoleClaim, PTContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}