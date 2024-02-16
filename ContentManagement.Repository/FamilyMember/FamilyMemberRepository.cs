using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
