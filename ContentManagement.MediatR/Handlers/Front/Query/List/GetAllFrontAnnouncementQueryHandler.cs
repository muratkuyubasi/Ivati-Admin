using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ContentManagement.MediatR.Queries;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontAnnouncementQueryHandler : IRequestHandler<GetAllFrontAnnouncementQuery, ServiceResponse<List<FrontAnnouncementDto>>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IMapper _mapper;

        public GetAllFrontAnnouncementQueryHandler(
            IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper)
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontAnnouncementDto>>> Handle(GetAllFrontAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontAnnouncementRepository.AllIncluding(i => i.FrontAnnouncementRecords.Where(x => x.LanguageCode == request.LanguageCode)).ToListAsync();
            return ServiceResponse<List<FrontAnnouncementDto>>.ReturnResultWith200(_mapper.Map<List<FrontAnnouncementDto>>(entities));
        }
    }
}
