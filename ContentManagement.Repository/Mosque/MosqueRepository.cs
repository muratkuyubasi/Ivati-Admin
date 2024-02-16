using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
