using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontGalleryRecordRepository : GenericRepository<FrontGalleryRecord, PTContext>,
          IFrontGalleryRecordRepository
    {
        public FrontGalleryRecordRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
