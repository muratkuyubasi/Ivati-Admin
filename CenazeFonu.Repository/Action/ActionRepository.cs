using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class ActionRepository : GenericRepository<Action, PTContext>,
          IActionRepository
    {
        public ActionRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
