using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class MemberTypeRepository : GenericRepository<MemberType, PTContext>,
          IMemberTypeRepository
    {
        public MemberTypeRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
