using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
