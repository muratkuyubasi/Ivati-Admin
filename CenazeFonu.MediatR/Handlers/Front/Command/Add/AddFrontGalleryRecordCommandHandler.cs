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
    public class AddFrontGalleryRecordCommandHandler : IRequestHandler<AddFrontGalleryRecordCommand, ServiceResponse<FrontGalleryRecordDto>>
    {
        private readonly IFrontGalleryRecordRepository _FrontGalleryRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontGalleryRecordCommandHandler> _logger;
        public AddFrontGalleryRecordCommandHandler(
           IFrontGalleryRecordRepository FrontGalleryRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontGalleryRecordCommandHandler> logger
            )
        {
            _FrontGalleryRecordRepository = FrontGalleryRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryRecordDto>> Handle(AddFrontGalleryRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGalleryRecord>(request);
            _FrontGalleryRecordRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryRecordDto>(entity);
            return ServiceResponse<FrontGalleryRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
