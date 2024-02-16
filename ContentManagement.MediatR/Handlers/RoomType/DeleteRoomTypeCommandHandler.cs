using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
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
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, ServiceResponse<RoomTypeDTO>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<RoomTypeDTO>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var data = await _roomTypeRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Bu ID'ye ait bir oda tipi bulunamadı!");
            }
            _roomTypeRepository.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<RoomTypeDTO>.ReturnResultWith200(_mapper.Map<RoomTypeDTO>(data));
        }
    }
}
