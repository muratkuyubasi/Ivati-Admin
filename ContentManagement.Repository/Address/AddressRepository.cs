using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class AddressRepository : GenericRepository<Address, PTContext>,
          IAddressRepository
    {
        public AddressRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
