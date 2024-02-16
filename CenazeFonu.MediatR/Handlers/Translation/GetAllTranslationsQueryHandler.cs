using AutoMapper;
using CenazeFonu.Data.Dto;
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
    public class GetAllTranslationsQueryHandler : IRequestHandler<GetTranslationsQuery, ServiceResponse<List<TranslationDTO>>>
    {
        private readonly ITranslationRepository repo;
        private readonly IMapper _map;

        public GetAllTranslationsQueryHandler(ITranslationRepository cityRepository, IMapper mapper)
        {
            repo = cityRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<List<TranslationDTO>>> Handle(GetTranslationsQuery request, CancellationToken cancellationToken)
        {
            var cities = await repo.All.Include(x => x.Language).OrderBy(X=>X.Order).ToListAsync();
            return ServiceResponse<List<TranslationDTO>>.ReturnResultWith200(_map.Map<List<TranslationDTO>>(cities));
        }
    }
}
