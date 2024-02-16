using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class CityRepository : GenericRepository<City, PTContext>,
          ICityRepository
    {
        public CityRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
