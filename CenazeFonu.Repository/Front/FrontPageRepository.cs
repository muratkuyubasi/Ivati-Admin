using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontPageRepository : GenericRepository<FrontPage, PTContext>,
          IFrontPageRepository
    {
        public FrontPageRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
