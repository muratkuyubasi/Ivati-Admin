using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDefaultAreaQueryHandler : IRequestHandler<GetDefaultAreaQuery, ServiceResponse<List<CityDTO>>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetDefaultAreaQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<CityDTO>>> Handle(GetDefaultAreaQuery request, CancellationToken cancellationToken)
        {
            string[] cities = { "Milano", "Como", "Novara", "Imperia", "Modena", "Venedik" };
            List<City> areas = new List<City>();
            foreach (var city in cities)
            {
                var data = _cityRepository.FindBy(X => X.Name == city).FirstOrDefault();
                if (data != null)
                {
                    areas.Add(data);
                }
            }
            return ServiceResponse<List<CityDTO>>.ReturnResultWith200(_mapper.Map<List<CityDTO>>(areas));
        }
    }
}
