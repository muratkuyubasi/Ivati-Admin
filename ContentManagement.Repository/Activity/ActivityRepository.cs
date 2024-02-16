using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class ActivityRepository : GenericRepository<Activity, PTContext>,
          IActivityRepository
    {
        public ActivityRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
