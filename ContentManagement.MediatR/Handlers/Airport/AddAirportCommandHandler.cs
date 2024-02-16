using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddAirportCommandHandler : IRequestHandler<AddAirportCommand, ServiceResponse<AirportDTO>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddAirportCommandHandler(IAirportRepository airportRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AirportDTO>> Handle(AddAirportCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Airport>(request);
            if (!String.IsNullOrWhiteSpace(model.Name))
            {
                _airportRepository.Add(model);
            }
            else return ServiceResponse<AirportDTO>.Return409("Havaalanı adı boş bırakılamaz!");
            
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AirportDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<AirportDTO>.ReturnResultWith200(_mapper.Map<AirportDTO>(model));
        }
    }
}
