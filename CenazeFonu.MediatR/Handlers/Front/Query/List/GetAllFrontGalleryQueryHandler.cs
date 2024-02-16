using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Repository;
using CenazeFonu.MediatR.Queries;

namespace CenazeFonu.Mediatr.Handlers
{
    public class GetAllFrontGalleryQueryHandler : IRequestHandler<GetAllFrontGalleryQuery, ServiceResponse<List<FrontGalleryDto>>>
    {
        private readonly IFrontGalleryRepository _FrontGalleryRepository;
        private readonly IMapper _mapper;

        public GetAllFrontGalleryQueryHandler(
            IFrontGalleryRepository FrontGalleryRepository,
            IMapper mapper)
        {
            _FrontGalleryRepository = FrontGalleryRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontGalleryDto>>> Handle(GetAllFrontGalleryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontGalleryRepository.All.ToListAsync();
            return ServiceResponse<List<FrontGalleryDto>>.ReturnResultWith200(_mapper.Map<List<FrontGalleryDto>>(entities));
        }
    }
}
