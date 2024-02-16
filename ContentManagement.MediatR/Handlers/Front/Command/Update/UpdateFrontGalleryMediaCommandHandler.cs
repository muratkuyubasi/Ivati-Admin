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
using System.IO;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateFrontGalleryMediaCommandHandler : IRequestHandler<UpdateFrontGalleryMediaCommand, ServiceResponse<FrontGalleryMediaDto>>
    {
        private readonly IFrontGalleryMediaRepository _FrontGalleryMediaRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontGalleryMediaCommandHandler> _logger;
        public UpdateFrontGalleryMediaCommandHandler(
           IFrontGalleryMediaRepository FrontGalleryMediaRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontGalleryMediaCommandHandler> logger
            )
        {
            _FrontGalleryMediaRepository = FrontGalleryMediaRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryMediaDto>> Handle(UpdateFrontGalleryMediaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGalleryMedia>(request);

            if (request.FormFile != null)
            {
                using (Stream fileStream = new FileStream(request.FileUrl, FileMode.Create))
                {
                    await request.FormFile.CopyToAsync(fileStream);
                }
            }

            _FrontGalleryMediaRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryMediaDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryMediaDto>(entity);
            return ServiceResponse<FrontGalleryMediaDto>.ReturnResultWith200(entityDto);
        }
    }
}
