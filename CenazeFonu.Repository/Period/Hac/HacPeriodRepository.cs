using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
