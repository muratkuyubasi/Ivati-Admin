using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class AgeRepository : GenericRepository<Age, PTContext>,
          IAgeRepository
    {
        public AgeRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
