using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class CitizenshipRepository : GenericRepository<Citizenship, PTContext>,
          ICitizenshipRepository
    {
        public CitizenshipRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
