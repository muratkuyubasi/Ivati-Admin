using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.Repository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteFrontPageRecordCommandHandler : IRequestHandler<DeleteFrontPageRecordCommand, ServiceResponse<FrontPageRecordDto>>
    {
        private readonly IFrontPageRecordRepository _FrontPageRecordRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontPageRecordCommandHandler> _logger;
        public DeleteFrontPageRecordCommandHandler(
           IFrontPageRecordRepository FrontPageRecordRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontPageRecordCommandHandler> logger
            )
        {
            _FrontPageRecordRepository = FrontPageRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontPageRecordDto>> Handle(DeleteFrontPageRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontPageRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            _FrontPageRecordRepository.Remove(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontPageRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontPageRecordDto>(entity);
            return ServiceResponse<FrontPageRecordDto>.ReturnResultWith204();
        }
    }
}
