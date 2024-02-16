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
    public class ClergyRepository : GenericRepository<Clergy, PTContext>, IClergyRepository
    {
        public ClergyRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
