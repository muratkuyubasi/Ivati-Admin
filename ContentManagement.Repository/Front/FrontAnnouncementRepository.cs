using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
