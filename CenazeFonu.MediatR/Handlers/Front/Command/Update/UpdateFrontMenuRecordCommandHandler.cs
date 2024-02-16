﻿using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using PT.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using Microsoft.Extensions.Logging;

namespace CenazeFonu.Mediatr.Handlers
{
    public class UpdateFrontMenuRecordCommandHandler : IRequestHandler<UpdateFrontMenuRecordCommand, ServiceResponse<FrontMenuRecordDto>>
    {
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontMenuRecordCommandHandler> _logger;
        public UpdateFrontMenuRecordCommandHandler(
           IFrontMenuRecordRepository FrontMenuRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontMenuRecordCommandHandler> logger
            )
        {
            _FrontMenuRecordRepository = FrontMenuRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontMenuRecordDto>> Handle(UpdateFrontMenuRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontMenuRecord>(request);
            _FrontMenuRecordRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuRecordDto>(entity);
            return ServiceResponse<FrontMenuRecordDto>.ReturnResultWith200(entityDto);
        }
    }
}