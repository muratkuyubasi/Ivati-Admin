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
using ContentManagement.Domain;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteFrontGalleryCommandHandler : IRequestHandler<DeleteFrontGalleryCommand, ServiceResponse<FrontGalleryDto>>
    {
        private readonly IFrontGalleryRepository _FrontGalleryRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontGalleryCommandHandler> _logger;
        public DeleteFrontGalleryCommandHandler(
           IFrontGalleryRepository FrontGalleryRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontGalleryCommandHandler> logger
            )
        {
            _FrontGalleryRepository = FrontGalleryRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryDto>> Handle(DeleteFrontGalleryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontGalleryRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            _FrontGalleryRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryDto>(entity);
            return ServiceResponse<FrontGalleryDto>.ReturnResultWith204();
        }
    }
}
