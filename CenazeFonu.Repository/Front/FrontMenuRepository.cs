using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontMenuRepository : GenericRepository<FrontMenu, PTContext>,
          IFrontMenuRepository
    {
        public FrontMenuRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
