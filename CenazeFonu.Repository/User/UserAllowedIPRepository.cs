using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
