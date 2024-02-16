using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
