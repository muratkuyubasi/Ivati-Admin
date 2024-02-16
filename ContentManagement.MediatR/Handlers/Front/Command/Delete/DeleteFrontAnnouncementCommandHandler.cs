using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;
using ContentManagement.Repository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteFrontAnnouncementCommandHandler : IRequestHandler<DeleteFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontAnnouncementCommandHandler> _logger;
        public DeleteFrontAnnouncementCommandHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontAnnouncementCommandHandler> logger
            )
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(DeleteFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontAnnouncementRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            _FrontAnnouncementRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementDto>(entity);
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith204();
        }
    }
}
