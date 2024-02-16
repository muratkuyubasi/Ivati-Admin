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
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, ServiceResponse<List<CityDTO>>>
    {
        private readonly ICityRepository repo;
        private readonly IMapper _map;

        public GetAllCitiesQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            repo = cityRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<List<CityDTO>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await repo.All.Include(x => x.Country).ToListAsync();
            return ServiceResponse<List<CityDTO>>.ReturnResultWith200(_map.Map<List<CityDTO>>(cities));
        }
    }
}
