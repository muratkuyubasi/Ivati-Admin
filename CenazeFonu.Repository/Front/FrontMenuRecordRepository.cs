using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontMenuRecordRepository : GenericRepository<FrontMenuRecord, PTContext>,
          IFrontMenuRecordRepository
    {
        public FrontMenuRecordRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
