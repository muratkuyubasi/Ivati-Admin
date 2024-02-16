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
    public class AddFrontAnnouncementCommandHandler : IRequestHandler<AddFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontAnnouncementCommandHandler> _logger;
        public AddFrontAnnouncementCommandHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontAnnouncementCommandHandler> logger
            )
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(AddFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CenazeFonu.Data.FrontAnnouncement>(request);
            entity.Code = Guid.NewGuid();
            _FrontAnnouncementRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementDto>(entity);
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(entityDto);
        }
    }
}
