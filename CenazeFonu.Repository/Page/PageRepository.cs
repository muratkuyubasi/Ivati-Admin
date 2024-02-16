using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
