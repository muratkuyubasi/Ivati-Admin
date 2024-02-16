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
    public class UpdateFrontPageRecordCommandHandler : IRequestHandler<UpdateFrontPageRecordCommand, ServiceResponse<FrontPageRecordDto>>
    {
        private readonly IFrontPageRecordRepository _FrontPageRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontPageRecordCommandHandler> _logger;
        public UpdateFrontPageRecordCommandHandler(
           IFrontPageRecordRepository FrontPageRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontPageRecordCommandHandler> logger
            )
        {
            _FrontPageRecordRepository = FrontPageRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontPageRecordDto>> Handle(UpdateFrontPageRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontPageRecord>(request);
            _FrontPageRecordRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontPageRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontPageRecordDto>(entity);
            return ServiceResponse<FrontPageRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
