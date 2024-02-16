using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFrontGalleryRecordQueryHandler : IRequestHandler<GetFrontGalleryRecordQuery, ServiceResponse<FrontGalleryRecordDto>>
    {
        private readonly IFrontGalleryRecordRepository _FrontGalleryRecordRepository;
        private readonly IMapper _mapper;

        public GetFrontGalleryRecordQueryHandler(
           IFrontGalleryRecordRepository FrontGalleryRecordRepository,
           IMapper mapper)
        {
            _FrontGalleryRecordRepository = FrontGalleryRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontGalleryRecordDto>> Handle(GetFrontGalleryRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = _FrontGalleryRecordRepository.FindBy(x=>x.Id == request.Id);
            if (entity != null)
                return ServiceResponse<FrontGalleryRecordDto>.ReturnResultWith200(_mapper.Map<FrontGalleryRecordDto>(entity));
            else
                return ServiceResponse<FrontGalleryRecordDto>.Return404();
        }
    }
}
