using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllRoomTypeQueryHandler : IRequestHandler<GetAllRoomTypesQuery, ServiceResponse<List<RoomTypeDTO>>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetAllRoomTypeQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<RoomTypeDTO>>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _roomTypeRepository.All.ToListAsync();
            return ServiceResponse<List<RoomTypeDTO>>.ReturnResultWith200(_mapper.Map<List<RoomTypeDTO>>(rooms));
        }
    }
}
