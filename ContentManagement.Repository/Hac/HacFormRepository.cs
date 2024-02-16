using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Repository
{
    public class HacFormRepository : GenericRepository<HacForm, PTContext>, IHacRepository
    {
        public HacFormRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
