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
    public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand, ServiceResponse<AirportDTO>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateAirportCommandHandler(IAirportRepository airportRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AirportDTO>> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
        {
            var model = await _airportRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<AirportDTO>.Return409("Bu ID'ye ait bir havaalanı bulunmamaktadır!");
            }
            if (!String.IsNullOrEmpty(request.Name))
            {
                model.Name = request.Name;
            }
            else return ServiceResponse<AirportDTO>.Return409("Havaalanı adı boş kalamaz!");

            _airportRepository.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AirportDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<AirportDTO>.ReturnResultWith200(_mapper.Map<AirportDTO>(model));
        }
    }
}
