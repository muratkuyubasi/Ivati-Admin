using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FamilyMemberRepository : GenericRepository<FamilyMember, PTContext>,
          IFamilyMemberRepository
    {
        public FamilyMemberRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
