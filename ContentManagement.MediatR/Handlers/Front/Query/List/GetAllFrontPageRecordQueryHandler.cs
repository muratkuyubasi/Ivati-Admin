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
    public class GetAllFrontPageRecordQueryHandler : IRequestHandler<GetAllFrontPageRecordQuery, ServiceResponse<List<FrontPageRecordDto>>>
    {
        private readonly IFrontPageRecordRepository _FrontPageRecordRepository;
        private readonly IMapper _mapper;

        public GetAllFrontPageRecordQueryHandler(
            IFrontPageRecordRepository FrontPageRecordRepository,
            IMapper mapper)
        {
            _FrontPageRecordRepository = FrontPageRecordRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontPageRecordDto>>> Handle(GetAllFrontPageRecordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontPageRecordRepository.All.ToListAsync();
            return ServiceResponse<List<FrontPageRecordDto>>.ReturnResultWith200(_mapper.Map<List<FrontPageRecordDto>>(entities));
        }
    }
}
