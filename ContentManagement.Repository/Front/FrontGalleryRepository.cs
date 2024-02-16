using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontGalleryRepository : GenericRepository<FrontGallery, PTContext>,
          IFrontGalleryRepository
    {
        public FrontGalleryRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
