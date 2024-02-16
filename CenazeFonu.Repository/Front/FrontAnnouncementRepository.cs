using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontAnnouncementRepository : GenericRepository<FrontAnnouncement, PTContext>,
          IFrontAnnouncementRepository
    {
        public FrontAnnouncementRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
