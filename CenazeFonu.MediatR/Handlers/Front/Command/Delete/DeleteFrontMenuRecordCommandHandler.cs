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
    public class DeleteFrontMenuRecordCommandHandler : IRequestHandler<DeleteFrontMenuRecordCommand, ServiceResponse<FrontMenuRecordDto>>
    {
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontMenuRecordCommandHandler> _logger;
        public DeleteFrontMenuRecordCommandHandler(
           IFrontMenuRecordRepository FrontMenuRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontMenuRecordCommandHandler> logger
            )
        {
            _FrontMenuRecordRepository = FrontMenuRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontMenuRecordDto>> Handle(DeleteFrontMenuRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontMenuRecordRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            _FrontMenuRecordRepository.Remove(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuRecordDto>(entity);
            return ServiceResponse<FrontMenuRecordDto>.ReturnResultWith204();
        }
    }
}
