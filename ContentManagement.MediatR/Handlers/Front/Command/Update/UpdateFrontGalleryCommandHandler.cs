using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using PT.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using Microsoft.Extensions.Logging;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateFrontGalleryCommandHandler : IRequestHandler<UpdateFrontGalleryCommand, ServiceResponse<FrontGalleryDto>>
    {
        private readonly IFrontGalleryRepository _FrontGalleryRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontGalleryCommandHandler> _logger;
        public UpdateFrontGalleryCommandHandler(
           IFrontGalleryRepository FrontGalleryRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontGalleryCommandHandler> logger
            )
        {
            _FrontGalleryRepository = FrontGalleryRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryDto>> Handle(UpdateFrontGalleryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGallery>(request);
            _FrontGalleryRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryDto>(entity);
            return ServiceResponse<FrontGalleryDto>.ReturnResultWith200(entityDto);
        }
    }
}
