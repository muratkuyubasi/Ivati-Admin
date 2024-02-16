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
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, ServiceResponse<List<CountryDTO>>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;

        public GetAllCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _repo = countryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<CountryDTO>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _repo.All.ToListAsync();
            return ServiceResponse<List<CountryDTO>>.ReturnResultWith200(_mapper.Map<List<CountryDTO>>(countries));
        }
    }
}
