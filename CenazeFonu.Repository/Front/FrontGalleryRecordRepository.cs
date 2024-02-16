using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
