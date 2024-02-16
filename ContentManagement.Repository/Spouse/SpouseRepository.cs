using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Repository
{
    public class SpouseRepository : GenericRepository<Spouse, PTContext>, ISpouseRepository
    {
        public SpouseRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
