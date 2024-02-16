using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontMenuRecordQueryHandler : IRequestHandler<GetAllFrontMenuRecordQuery, ServiceResponse<List<FrontMenuRecordDto>>>
    {
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IMapper _mapper;

        public GetAllFrontMenuRecordQueryHandler(
            IFrontMenuRecordRepository FrontMenuRecordRepository,
            IMapper mapper)
        {
            _FrontMenuRecordRepository = FrontMenuRecordRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontMenuRecordDto>>> Handle(GetAllFrontMenuRecordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontMenuRecordRepository.All.Include(x=>x.FrontMenu).ThenInclude(x=>x.FrontPage).ToListAsync();
            return ServiceResponse<List<FrontMenuRecordDto>>.ReturnResultWith200(_mapper.Map<List<FrontMenuRecordDto>>(entities));
        }
    }
}
