using AutoMapper;

using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Repository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Domain;

namespace CenazeFonu.Mediatr.Handlers
{
    public class DeleteFrontAnnouncementRecordCommandHandler : IRequestHandler<DeleteFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontAnnouncementRecordCommandHandler> _logger;
        public DeleteFrontAnnouncementRecordCommandHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(DeleteFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontAnnouncementRecordRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            _FrontAnnouncementRecordRepository.Remove(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementRecordDto>(entity);
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith204();
        }
    }
}
