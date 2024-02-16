using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.Mediatr.Handlers
{
    public class GetFrontGalleryQueryHandler : IRequestHandler<GetFrontGalleryQuery, ServiceResponse<FrontGalleryDto>>
    {
        private readonly IFrontGalleryRepository _FrontGalleryRepository;
        private readonly IMapper _mapper;

        public GetFrontGalleryQueryHandler(
           IFrontGalleryRepository FrontGalleryRepository,
           IMapper mapper)
        {
            _FrontGalleryRepository = FrontGalleryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontGalleryDto>> Handle(GetFrontGalleryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontGalleryRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontGalleryDto>.ReturnResultWith200(_mapper.Map<FrontGalleryDto>(entity));
            else
                return ServiceResponse<FrontGalleryDto>.Return404();
        }
    }
}
