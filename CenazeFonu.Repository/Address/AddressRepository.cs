using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
