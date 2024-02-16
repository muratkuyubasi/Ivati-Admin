using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.Repository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteFrontGalleryRecordCommandHandler : IRequestHandler<DeleteFrontGalleryRecordCommand, ServiceResponse<FrontGalleryRecordDto>>
    {
        private readonly IFrontGalleryRecordRepository _FrontGalleryRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontGalleryRecordCommandHandler> _logger;
        public DeleteFrontGalleryRecordCommandHandler(
           IFrontGalleryRecordRepository FrontGalleryRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontGalleryRecordCommandHandler> logger
            )
        {
            _FrontGalleryRecordRepository = FrontGalleryRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryRecordDto>> Handle(DeleteFrontGalleryRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontGalleryRecordRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            _FrontGalleryRecordRepository.Remove(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryRecordDto>(entity);
            return ServiceResponse<FrontGalleryRecordDto>.ReturnResultWith204();
        }
    }
}
