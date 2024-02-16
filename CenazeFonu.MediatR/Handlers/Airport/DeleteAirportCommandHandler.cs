using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand, ServiceResponse<AirportDTO>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteAirportCommandHandler(IAirportRepository airportRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AirportDTO>> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
        {
            var data = await _airportRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AirportDTO>.Return409("Bu ID'ye ait bir havaalanı bulunamadı!");
            }
            _airportRepository.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AirportDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AirportDTO>.ReturnResultWith200(_mapper.Map<AirportDTO>(data));
        }
    }
}
