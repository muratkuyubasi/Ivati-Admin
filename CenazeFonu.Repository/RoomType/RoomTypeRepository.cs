using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public class RoomTypeRepository : GenericRepository<RoomType, PTContext>, IRoomTypeRepository
    {
        public RoomTypeRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
