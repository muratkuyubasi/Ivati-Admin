using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class PageActionRepository : GenericRepository<PageAction, PTContext>,
        IPageActionRepository
    {
        public PageActionRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
