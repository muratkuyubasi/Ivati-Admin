using AutoMapper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFrontAnnouncementQueryHandler : IRequestHandler<GetFrontAnnouncementQuery, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IMapper _mapper;

        public GetFrontAnnouncementQueryHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
           IMapper mapper)
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(GetFrontAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontAnnouncementRepository.FindByInclude(x => x.Code == request.Code, i=> i.FrontAnnouncementRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementDto>(entity));
            else
                return ServiceResponse<FrontAnnouncementDto>.Return404();
        }
    }
}
