using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontGalleryMediaRepository : GenericRepository<FrontGalleryMedia, PTContext>,
          IFrontGalleryMediaRepository
    {
        public FrontGalleryMediaRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
