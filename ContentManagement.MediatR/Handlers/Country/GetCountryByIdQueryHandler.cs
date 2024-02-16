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
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, ServiceResponse<List<CountryDTO>>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;

        public GetCountryByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _repo = countryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<CountryDTO>>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.All.Where(x => x.Id == request.Id).Include(x => x.Cities).ToListAsync();
            if (data == null)
            {
                return ServiceResponse<List<CountryDTO>>.Return409("Bu ID'ye ait bir ülke bulunmamaktadır!");
            }
            else return ServiceResponse<List<CountryDTO>>.ReturnResultWith200(_mapper.Map<List<CountryDTO>>(data));
        }
    }
}
