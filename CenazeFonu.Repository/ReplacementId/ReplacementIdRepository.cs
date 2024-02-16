using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class ReplacementIdRepository : GenericRepository<ReplacementId, PTContext>,
          IReplacementIdRepository
    {
        public ReplacementIdRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
