using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
