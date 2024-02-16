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
using CenazeFonu.Data;
using System.Linq;

namespace CenazeFonu.MediatR.Handlers
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
