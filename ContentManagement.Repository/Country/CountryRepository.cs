using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
