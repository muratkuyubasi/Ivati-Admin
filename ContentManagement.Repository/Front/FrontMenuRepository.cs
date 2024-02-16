using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontMenuRepository : GenericRepository<FrontMenu, PTContext>,
          IFrontMenuRepository
    {
        public FrontMenuRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
