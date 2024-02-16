using AutoMapper;
using ContentManagement.Data.Dto;
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
    public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, ServiceResponse<List<LanguageDTO>>>
    {
        private readonly ILanguageRepository repo;
        private readonly IMapper _map;

        public GetAllLanguagesQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            repo = languageRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<List<LanguageDTO>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            var cities = await repo.All.ToListAsync();
            return ServiceResponse<List<LanguageDTO>>.ReturnResultWith200(_map.Map<List<LanguageDTO>>(cities));
        }
    }
}
