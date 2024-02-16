using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FoundationPublicationRepository : GenericRepository<FoundationPublication, PTContext>,
          IFoundationPublicationRepository
    {
        public FoundationPublicationRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
