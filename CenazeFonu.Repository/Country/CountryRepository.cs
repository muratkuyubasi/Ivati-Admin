using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class CountryRepository : GenericRepository<Country, PTContext>,
          ICountryRepository
    {
        public CountryRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
