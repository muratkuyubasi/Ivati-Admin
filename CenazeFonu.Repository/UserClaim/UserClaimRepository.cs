using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class UserClaimRepository : GenericRepository<UserClaim, PTContext>,
           IUserClaimRepository
    {
        public UserClaimRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}