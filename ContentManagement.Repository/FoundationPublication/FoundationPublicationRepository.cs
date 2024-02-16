using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
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
