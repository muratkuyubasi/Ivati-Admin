using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
