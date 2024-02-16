using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddRoomTypeCommandHandler : IRequestHandler<AddRoomTypeCommand, ServiceResponse<RoomTypeDTO>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> uow;

        public AddRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
            this.uow = uow;
        }

        public async Task<ServiceResponse<RoomTypeDTO>> Handle(AddRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<RoomType>(request);
            if (!String.IsNullOrWhiteSpace(model.RoomTypes))
            {
                _roomTypeRepository.Add(model);
            }
            else
                return ServiceResponse<RoomTypeDTO>.Return409("Oda tipi boş geçilemez!");
            
            if (await uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<RoomTypeDTO>.ReturnResultWith200(_mapper.Map<RoomTypeDTO>(model));
        }
    }
}
