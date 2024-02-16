using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class ServiceRepository : GenericRepository<Services, PTContext>,
          IServiceRepository
    {
        public ServiceRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
