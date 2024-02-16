using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class HacPeriodRepository : GenericRepository<HacPeriod, PTContext>,
          IHacPeriodRepository
    {
        public HacPeriodRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
