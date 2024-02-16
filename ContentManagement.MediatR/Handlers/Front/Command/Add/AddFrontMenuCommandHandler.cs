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
using ContentManagement.Data;
using System.Linq;

namespace ContentManagement.MediatR.Handlers
{
    public class AddFrontMenuCommandHandler : IRequestHandler<AddFrontMenuCommand, ServiceResponse<FrontMenuDto>>
    {
        private readonly IFrontMenuRepository _FrontMenuRepository;
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontMenuCommandHandler> _logger;
        public AddFrontMenuCommandHandler(
           IFrontMenuRepository FrontMenuRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontMenuCommandHandler> logger
,
            IFrontMenuRecordRepository frontMenuRecordRepository)
        {
            _FrontMenuRepository = FrontMenuRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _FrontMenuRecordRepository = frontMenuRecordRepository;
        }
        public async Task<ServiceResponse<FrontMenuDto>> Handle(AddFrontMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FrontMenu>(request);
            entity.Code = Guid.NewGuid();
            _FrontMenuRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuDto>(entity);
            return ServiceResponse<FrontMenuDto>.ReturnResultWith200(entityDto);
        }
    }
}
