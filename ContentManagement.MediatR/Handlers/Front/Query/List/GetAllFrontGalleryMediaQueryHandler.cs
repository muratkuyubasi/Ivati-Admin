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
    public class GetAllFrontGalleryMediaQueryHandler : IRequestHandler<GetAllFrontGalleryMediaQuery, ServiceResponse<List<FrontGalleryMediaDto>>>
    {
        private readonly IFrontGalleryMediaRepository _FrontGalleryMediaRepository;
        private readonly IMapper _mapper;

        public GetAllFrontGalleryMediaQueryHandler(
            IFrontGalleryMediaRepository FrontGalleryMediaRepository,
            IMapper mapper)
        {
            _FrontGalleryMediaRepository = FrontGalleryMediaRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontGalleryMediaDto>>> Handle(GetAllFrontGalleryMediaQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontGalleryMediaRepository.All.ToListAsync();
            return ServiceResponse<List<FrontGalleryMediaDto>>.ReturnResultWith200(_mapper.Map<List<FrontGalleryMediaDto>>(entities));
        }
    }
}
