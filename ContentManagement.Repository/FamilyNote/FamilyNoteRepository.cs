using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class FamilyNoteRepository : GenericRepository<FamilyNote, PTContext>,
          IFamilyNoteRepository
    {
        public FamilyNoteRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
