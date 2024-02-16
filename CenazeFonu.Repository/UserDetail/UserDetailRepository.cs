using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
