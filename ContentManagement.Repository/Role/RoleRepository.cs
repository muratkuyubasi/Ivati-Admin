using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public  class RoleRepository : GenericRepository<Role, PTContext>,
          IRoleRepository
    {
        public RoleRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}