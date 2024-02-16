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
    public class GetAllAirportsQueryHandler : IRequestHandler<GetAllAirportsQuery, ServiceResponse<List<AirportDTO>>>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public GetAllAirportsQueryHandler(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<AirportDTO>>> Handle(GetAllAirportsQuery request, CancellationToken cancellationToken)
        {
            var airports = await _airportRepository.All.ToListAsync();
            return ServiceResponse<List<AirportDTO>>.ReturnResultWith200(_mapper.Map<List<AirportDTO>>(airports));
        }
    }
}
