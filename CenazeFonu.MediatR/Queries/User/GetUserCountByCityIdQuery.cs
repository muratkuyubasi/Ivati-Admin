using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries.User
{
    public class GetUserCountByCityIdQuery : IRequest<Object>
    {
        public int CityId { get; set; }
    }
}
