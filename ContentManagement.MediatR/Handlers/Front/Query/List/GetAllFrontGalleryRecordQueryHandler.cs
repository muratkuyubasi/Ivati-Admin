using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;
using ContentManagement.Repository;
using ContentManagement.MediatR.Queries;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFrontGalleryRecordQueryHandler : IRequestHandler<GetAllFrontGalleryRecordQuery, ServiceResponse<List<FrontGalleryRecordDto>>>
    {
        private readonly IFrontGalleryRecordRepository _FrontGalleryRecordRepository;
        private readonly IMapper _mapper;

        public GetAllFrontGalleryRecordQueryHandler(
            IFrontGalleryRecordRepository FrontGalleryRecordRepository,
            IMapper mapper)
        {
            _FrontGalleryRecordRepository = FrontGalleryRecordRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<FrontGalleryRecordDto>>> Handle(GetAllFrontGalleryRecordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontGalleryRecordRepository.All.ToListAsync();
            return ServiceResponse<List<FrontGalleryRecordDto>>.ReturnResultWith200(_mapper.Map<List<FrontGalleryRecordDto>>(entities));
        }
    }
}
