using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using ContentManagement.Data;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontMenuQueryHandler : IRequestHandler<GetAllFrontMenuQuery, ServiceResponse<List<FrontMenuDto>>>
    {
        private readonly IFrontMenuRepository _FrontMenuRepository;
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IMapper _mapper;

        public GetAllFrontMenuQueryHandler(
            IFrontMenuRepository FrontMenuRepository,
            IMapper mapper,
            IFrontMenuRecordRepository frontMenuRecordRepository)
        {
            _FrontMenuRepository = FrontMenuRepository;
            _mapper = mapper;
            _FrontMenuRecordRepository = frontMenuRecordRepository;
        }
        public async Task<ServiceResponse<List<FrontMenuDto>>> Handle(GetAllFrontMenuQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontMenuRepository
                .AllIncluding(i => i.FrontMenuRecords.Where(x => x.LanguageCode == request.LanguageCode))
                .Include(x => x.Parent)
                .ThenInclude(x => x.FrontMenuRecords)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return ServiceResponse<List<FrontMenuDto>>.ReturnResultWith200(_mapper.Map<List<FrontMenuDto>>(entities));
        }
    }
}
