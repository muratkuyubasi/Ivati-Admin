using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontAnnouncementRecordRepository : GenericRepository<FrontAnnouncementRecord, PTContext>,
          IFrontAnnouncementRecordRepository
    {
        public FrontAnnouncementRecordRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
