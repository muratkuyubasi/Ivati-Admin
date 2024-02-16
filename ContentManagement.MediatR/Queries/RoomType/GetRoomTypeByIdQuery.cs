using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Queries
{
    public class GetRoomTypeByIdQuery : IRequest<ServiceResponse<RoomTypeDTO>>
    {
        public int Id { get; set; }
    }
}
