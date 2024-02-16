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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, ServiceResponse<RoomTypeDTO>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetRoomTypeByIdQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RoomTypeDTO>> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _roomTypeRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Bu ID'ye ait bir oda tipi bulunmamaktadır!");
            }
            else return ServiceResponse<RoomTypeDTO>.ReturnResultWith200(_mapper.Map<RoomTypeDTO>(data));
        }
    }
}
