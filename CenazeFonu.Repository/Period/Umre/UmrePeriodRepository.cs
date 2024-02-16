using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
