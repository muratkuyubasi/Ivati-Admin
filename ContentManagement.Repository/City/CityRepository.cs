using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
