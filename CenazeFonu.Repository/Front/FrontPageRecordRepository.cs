using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontPageRecordRepository : GenericRepository<FrontPageRecord, PTContext>,
          IFrontPageRecordRepository
    {
        public FrontPageRecordRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
