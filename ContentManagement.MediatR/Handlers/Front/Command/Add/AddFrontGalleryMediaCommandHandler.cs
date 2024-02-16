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
using System.IO;

namespace ContentManagement.MediatR.Handlers
{
    public class AddFrontGalleryMediaCommandHandler : IRequestHandler<AddFrontGalleryMediaCommand, ServiceResponse<FrontGalleryMediaDto>>
    {
        private readonly IFrontGalleryMediaRepository _FrontGalleryMediaRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontGalleryMediaCommandHandler> _logger;
        public AddFrontGalleryMediaCommandHandler(
           IFrontGalleryMediaRepository FrontGalleryMediaRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontGalleryMediaCommandHandler> logger
            )
        {
            _FrontGalleryMediaRepository = FrontGalleryMediaRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontGalleryMediaDto>> Handle(AddFrontGalleryMediaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontGalleryMedia>(request);

            if (request.FormFile != null)
            {
                using (Stream fileStream = new FileStream(request.FileUrl, FileMode.Create))
                {
                    await request.FormFile.CopyToAsync(fileStream);
                }
            }

            _FrontGalleryMediaRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontGalleryMediaDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontGalleryMediaDto>(entity);
            return ServiceResponse<FrontGalleryMediaDto>.ReturnResultWith200(entityDto);
        }
    }
}
