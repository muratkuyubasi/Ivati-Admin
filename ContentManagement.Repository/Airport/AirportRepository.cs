using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class AirportRepository : GenericRepository<Airport, PTContext>,
          IAirportRepository
    {
        public AirportRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
