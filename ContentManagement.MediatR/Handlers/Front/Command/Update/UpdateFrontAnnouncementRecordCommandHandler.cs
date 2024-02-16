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
    public class UpdateFrontAnnouncementRecordCommandHandler : IRequestHandler<UpdateFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontAnnouncementRecordCommandHandler> _logger;
        public UpdateFrontAnnouncementRecordCommandHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(UpdateFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontAnnouncementRecord>(request);
            _FrontAnnouncementRecordRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementRecordDto>(entity);
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
