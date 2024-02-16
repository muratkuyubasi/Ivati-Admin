using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
