using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Repository;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontAnnouncementRecordQueryHandler : IRequestHandler<GetAllFrontAnnouncementRecordQuery, ServiceResponse<List<FrontAnnouncementRecordDto>>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IMapper _mapper;

        public GetAllFrontAnnouncementRecordQueryHandler(
            IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper)
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontAnnouncementRecordDto>>> Handle(GetAllFrontAnnouncementRecordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontAnnouncementRecordRepository.All.ToListAsync();
            return ServiceResponse<List<FrontAnnouncementRecordDto>>.ReturnResultWith200(_mapper.Map<List<FrontAnnouncementRecordDto>>(entities));
        }
    }
}
