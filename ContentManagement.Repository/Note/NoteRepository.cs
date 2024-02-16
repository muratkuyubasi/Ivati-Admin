using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class NoteRepository : GenericRepository<Note, PTContext>,
          INoteRepository
    {
        public NoteRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
