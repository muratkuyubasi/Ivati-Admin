using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ContentManagement.MediatR.Commands;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.Repository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class AddFrontAnnouncementRecordCommandHandler : IRequestHandler<AddFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontAnnouncementRecordCommandHandler> _logger;
        public AddFrontAnnouncementRecordCommandHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(AddFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontAnnouncementRecord>(request);
            _FrontAnnouncementRecordRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementRecordDto>(entity);
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
