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
    public class AddFrontMenuRecordCommandHandler : IRequestHandler<AddFrontMenuRecordCommand, ServiceResponse<FrontMenuRecordDto>>
    {
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontMenuRecordCommandHandler> _logger;
        public AddFrontMenuRecordCommandHandler(
           IFrontMenuRecordRepository FrontMenuRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontMenuRecordCommandHandler> logger
            )
        {
            _FrontMenuRecordRepository = FrontMenuRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontMenuRecordDto>> Handle(AddFrontMenuRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontMenuRecord>(request);
            _FrontMenuRecordRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuRecordDto>(entity);
            return ServiceResponse<FrontMenuRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}
