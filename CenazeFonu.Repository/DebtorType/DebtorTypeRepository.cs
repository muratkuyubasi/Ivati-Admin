using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class DebtorTypeRepository : GenericRepository<DebtorType, PTContext>,
          IDebtorTypeRepository
    {
        public DebtorTypeRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
