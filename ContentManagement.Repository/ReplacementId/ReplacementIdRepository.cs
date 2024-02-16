using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class ReplacementIdRepository : GenericRepository<ReplacementId, PTContext>,
          IReplacementIdRepository
    {
        public ReplacementIdRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
