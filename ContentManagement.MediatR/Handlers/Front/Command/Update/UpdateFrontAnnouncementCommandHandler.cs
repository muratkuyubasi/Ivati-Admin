using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using PT.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using Microsoft.Extensions.Logging;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateFrontAnnouncementCommandHandler : IRequestHandler<UpdateFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontAnnouncementCommandHandler> _logger;
        public UpdateFrontAnnouncementCommandHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontAnnouncementCommandHandler> logger
            )
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(UpdateFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontAnnouncement>(request);
            _FrontAnnouncementRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementDto>(entity);
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(entityDto);
        }
    }
}
