using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using PT.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using Microsoft.Extensions.Logging;

namespace CenazeFonu.Mediatr.Handlers
{
    public class UpdateFrontGalleryRecordCommandHandler : IRequestHandler<UpdateFrontGalleryRecordCommand, ServiceResponse<FrontGalleryRecordDto>>
    {
        private readonly IFrontGalleryRecordRepository _FrontGalleryRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontGalleryRecordCommandHandler> _logger;
        public UpdateFrontGalleryRecordCommandHandler(
           IFrontGalleryRecordRepository FrontGalleryRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontGalleryRecordCommandHandler> logger
            )
        {
            _FrontGalleryRecordRepository = FrontGalleryRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryRecordDto>> Handle(UpdateFrontGalleryRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGalleryRecord>(request);
            _FrontGalleryRecordRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryRecordDto>(entity);
            return ServiceResponse<FrontGalleryRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
