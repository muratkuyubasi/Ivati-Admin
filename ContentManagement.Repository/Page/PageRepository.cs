using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class PageRepository : GenericRepository<Page, PTContext>,
          IPageRepository
    {
        public PageRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
