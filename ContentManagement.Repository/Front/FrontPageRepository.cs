using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontPageRepository : GenericRepository<FrontPage, PTContext>,
          IFrontPageRepository
    {
        public FrontPageRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
