using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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