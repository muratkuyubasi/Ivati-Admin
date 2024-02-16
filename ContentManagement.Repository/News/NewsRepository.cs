using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class NewsRepository : GenericRepository<News, PTContext>,
          INewsRepository
    {
        public NewsRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
