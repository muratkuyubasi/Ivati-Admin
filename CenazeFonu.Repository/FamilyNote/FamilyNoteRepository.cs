using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
