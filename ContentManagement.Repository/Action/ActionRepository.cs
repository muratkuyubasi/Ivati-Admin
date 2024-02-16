using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class ActionRepository : GenericRepository<Action, PTContext>,
          IActionRepository
    {
        public ActionRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
