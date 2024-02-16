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
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, ServiceResponse<CityDTO>>
    {
        private readonly ICityRepository repo;
        private readonly IMapper _map;

        public GetCityByIdQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            repo = cityRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<CityDTO>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<CityDTO>.Return409("Bu ID'ye ait bir şehir bulunmamaktadır!");
            }
            else return ServiceResponse<CityDTO>.ReturnResultWith200(_map.Map<CityDTO>(data));
        }
    }
}
