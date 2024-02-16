using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class CitizenshipRepository : GenericRepository<Citizenship, PTContext>,
          ICitizenshipRepository
    {
        public CitizenshipRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
