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
    public class AssociationRepository : GenericRepository<Association, PTContext>, IAssociationRepository
    {
        public AssociationRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
