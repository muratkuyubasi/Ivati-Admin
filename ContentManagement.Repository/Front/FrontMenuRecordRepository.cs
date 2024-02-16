using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FrontMenuRecordRepository : GenericRepository<FrontMenuRecord, PTContext>,
          IFrontMenuRecordRepository
    {
        public FrontMenuRecordRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
