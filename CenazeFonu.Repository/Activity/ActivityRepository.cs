using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
