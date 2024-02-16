using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFrontAnnouncementRecordQueryHandler : IRequestHandler<GetFrontAnnouncementRecordQuery, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IMapper _mapper;

        public GetFrontAnnouncementRecordQueryHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
           IMapper mapper)
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(GetFrontAnnouncementRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = _FrontAnnouncementRecordRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (entity != null)
                return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementRecordDto>(entity));
            else
                return ServiceResponse<FrontAnnouncementRecordDto>.Return404();
        }
    }
}
