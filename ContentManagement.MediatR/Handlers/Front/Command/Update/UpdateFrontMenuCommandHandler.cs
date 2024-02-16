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
    public class UpdateFrontMenuCommandHandler : IRequestHandler<UpdateFrontMenuCommand, ServiceResponse<FrontMenuDto>>
    {
        private readonly IFrontMenuRepository _FrontMenuRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontMenuCommandHandler> _logger;
        public UpdateFrontMenuCommandHandler(
           IFrontMenuRepository FrontMenuRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontMenuCommandHandler> logger
            )
        {
            _FrontMenuRepository = FrontMenuRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontMenuDto>> Handle(UpdateFrontMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontMenu>(request);
            _FrontMenuRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuDto>(entity);
            return ServiceResponse<FrontMenuDto>.ReturnResultWith200(entityDto);
        }
    }
}
