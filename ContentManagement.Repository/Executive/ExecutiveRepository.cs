using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class ExecutiveRepository : GenericRepository<Executive, PTContext>,
          IExecutiveRepository
    {
        public ExecutiveRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
