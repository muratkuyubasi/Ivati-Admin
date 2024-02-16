using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
