using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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