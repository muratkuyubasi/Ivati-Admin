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
    public class GetTranslationByIdQueryHandler : IRequestHandler<GetTranslationByIdQuery, ServiceResponse<TranslationDTO>>
    {
        private readonly ITranslationRepository repo;
        private readonly IMapper _map;

        public GetTranslationByIdQueryHandler(ITranslationRepository translationRepository, IMapper mapper)
        {
            repo = translationRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<TranslationDTO>> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<TranslationDTO>.Return409("Bu ID'ye ait bir çeviri bulunmamaktadır!");
            }
            else return ServiceResponse<TranslationDTO>.ReturnResultWith200(_map.Map<TranslationDTO>(data));
        }
    }
}
