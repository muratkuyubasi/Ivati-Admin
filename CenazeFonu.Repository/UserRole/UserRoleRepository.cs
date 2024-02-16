using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
