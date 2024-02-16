using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontPageQueryHandler : IRequestHandler<GetAllFrontPageQuery, ServiceResponse<List<FrontPageDto>>>
    {
        private readonly IFrontPageRepository _FrontPageRepository;
        private readonly IMapper _mapper;

        public GetAllFrontPageQueryHandler(
            IFrontPageRepository FrontPageRepository,
            IMapper mapper)
        {
            _FrontPageRepository = FrontPageRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontPageDto>>> Handle(GetAllFrontPageQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontPageRepository.AllIncluding(i=>i.FrontPageRecords.Where(x=>x.LanguageCode == request.LanguageCode)).Where(x=>!x.IsDeleted).ToListAsync();
            return ServiceResponse<List<FrontPageDto>>.ReturnResultWith200(_mapper.Map<List<FrontPageDto>>(entities));
        }
    }
}
