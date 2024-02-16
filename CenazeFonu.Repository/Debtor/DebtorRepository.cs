using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class DebtorRepository : GenericRepository<Debtor, PTContext>,
          IDebtorRepository
    {
        public DebtorRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
