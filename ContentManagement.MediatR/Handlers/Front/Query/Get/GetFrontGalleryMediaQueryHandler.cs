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
    public class GetFrontGalleryMediaQueryHandler : IRequestHandler<GetFrontGalleryMediaQuery, ServiceResponse<FrontGalleryMediaDto>>
    {
        private readonly IFrontGalleryMediaRepository _FrontGalleryMediaRepository;
        private readonly IMapper _mapper;

        public GetFrontGalleryMediaQueryHandler(
           IFrontGalleryMediaRepository FrontGalleryMediaRepository,
           IMapper mapper)
        {
            _FrontGalleryMediaRepository = FrontGalleryMediaRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontGalleryMediaDto>> Handle(GetFrontGalleryMediaQuery request, CancellationToken cancellationToken)
        {
            var entity = _FrontGalleryMediaRepository.FindBy(x => x.Id == request.Id);
            if (entity != null)
                return ServiceResponse<FrontGalleryMediaDto>.ReturnResultWith200(_mapper.Map<FrontGalleryMediaDto>(entity));
            else
                return ServiceResponse<FrontGalleryMediaDto>.Return404();
        }
    }
}
