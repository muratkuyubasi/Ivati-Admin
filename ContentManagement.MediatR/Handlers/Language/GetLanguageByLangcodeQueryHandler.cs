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
    public class GetLanguageByLangcodeQueryHandler : IRequestHandler<GetLanguageByLangcodeQuery, ServiceResponse<LanguageDTO>>
    {
        private readonly ILanguageRepository repo;
        private readonly IMapper _map;

        public GetLanguageByLangcodeQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            repo = languageRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<LanguageDTO>> Handle(GetLanguageByLangcodeQuery request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Langcode == request.Langcode).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<LanguageDTO>.Return409("Bu dil koduna ait bir dil bulunmamaktadır!");
            }
            else return ServiceResponse<LanguageDTO>.ReturnResultWith200(_map.Map<LanguageDTO>(data));
        }
    }
}
