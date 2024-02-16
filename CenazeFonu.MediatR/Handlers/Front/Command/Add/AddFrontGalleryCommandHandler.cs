using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Domain;

namespace CenazeFonu.MediatR.Handlers
{
    public class AddFrontGalleryCommandHandler : IRequestHandler<AddFrontGalleryCommand, ServiceResponse<FrontGalleryDto>>
    {
        private readonly IFrontGalleryRepository _FrontGalleryRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontGalleryCommandHandler> _logger;
        public AddFrontGalleryCommandHandler(
           IFrontGalleryRepository FrontGalleryRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontGalleryCommandHandler> logger
            )
        {
            _FrontGalleryRepository = FrontGalleryRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryDto>> Handle(AddFrontGalleryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGallery>(request);
            entity.Code = Guid.NewGuid();
            _FrontGalleryRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryDto>(entity);
            return ServiceResponse<FrontGalleryDto>.ReturnResultWith200(entityDto);
        }
    }
}
