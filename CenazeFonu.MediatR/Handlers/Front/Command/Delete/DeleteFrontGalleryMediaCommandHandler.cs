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
    public class DeleteFrontGalleryMediaCommandHandler : IRequestHandler<DeleteFrontGalleryMediaCommand, ServiceResponse<FrontGalleryMediaDto>>
    {
        private readonly IFrontGalleryMediaRepository _FrontGalleryMediaRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontGalleryMediaCommandHandler> _logger;
        public DeleteFrontGalleryMediaCommandHandler(
           IFrontGalleryMediaRepository FrontGalleryMediaRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontGalleryMediaCommandHandler> logger
            )
        {
            _FrontGalleryMediaRepository = FrontGalleryMediaRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryMediaDto>> Handle(DeleteFrontGalleryMediaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontGalleryMediaRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            _FrontGalleryMediaRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryMediaDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryMediaDto>(entity);
            return ServiceResponse<FrontGalleryMediaDto>.ReturnResultWith204();
        }
    }
}
