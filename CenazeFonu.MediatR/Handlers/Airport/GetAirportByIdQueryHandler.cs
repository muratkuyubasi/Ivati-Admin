using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
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
    public class GetAirportByIdQueryHandler : IRequestHandler<GetAirportByIdQuery, ServiceResponse<AirportDTO>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public GetAirportByIdQueryHandler(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AirportDTO>> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _airportRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AirportDTO>.Return409("Bu ID'ye ait bir havaalanı bulunmamaktadır!");
            }
            else return ServiceResponse<AirportDTO>.ReturnResultWith200(_mapper.Map<AirportDTO>(data));
        }
    }
}
