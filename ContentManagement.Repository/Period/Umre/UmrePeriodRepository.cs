using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class UmrePeriodRepository : GenericRepository<UmrePeriod, PTContext>,
          IUmrePeriodRepository
    {
        public UmrePeriodRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
