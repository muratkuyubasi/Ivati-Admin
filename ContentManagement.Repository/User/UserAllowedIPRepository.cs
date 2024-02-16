using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class UserAllowedIPRepository : GenericRepository<UserAllowedIP, PTContext>,
        IUserAllowedIPRepository
    {
        public UserAllowedIPRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
