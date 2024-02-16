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
    public class GetCountryByLangcodeQueryHandler : IRequestHandler<GetCountryByLangcodeQuery, ServiceResponse<List<CountryDTO>>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;

        public GetCountryByLangcodeQueryHandler(ICountryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<CountryDTO>>> Handle(GetCountryByLangcodeQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.All.Where(x => x.Langcode == request.Langcode).Include(x => x.Cities).ToListAsync();
            if (data == null)
            {
                return ServiceResponse<List<CountryDTO>>.Return409("Bu koda göre bir ülke bulunmamaktadır!");
            }
            else return ServiceResponse<List<CountryDTO>>.ReturnResultWith200(_mapper.Map<List<CountryDTO>>(data));
        }
    }
}
