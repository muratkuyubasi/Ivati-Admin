using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Domain.Migrations;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, ServiceResponse<RoomTypeDTO>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<RoomTypeDTO>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var model = await _roomTypeRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Bu ID'ye ait bir oda tipi bulunmamaktadır!");
            }
            if (!String.IsNullOrEmpty(request.RoomTypes))
            {
                model.RoomTypes = request.RoomTypes;
            }
            else return ServiceResponse<RoomTypeDTO>.Return409("Oda tipi boş kalamaz!");

            _roomTypeRepository.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RoomTypeDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<RoomTypeDTO>.ReturnResultWith200(_mapper.Map<RoomTypeDTO>(model));
        }
    }
}
