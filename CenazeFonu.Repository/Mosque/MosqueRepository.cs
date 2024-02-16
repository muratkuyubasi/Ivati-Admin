using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class MosqueRepository : GenericRepository<Mosque, PTContext>,
          IMosqueRepository
    {
        public MosqueRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
