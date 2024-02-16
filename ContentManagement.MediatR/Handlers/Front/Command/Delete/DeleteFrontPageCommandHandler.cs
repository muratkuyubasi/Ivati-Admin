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
    public class DeleteFrontPageCommandHandler : IRequestHandler<DeleteFrontPageCommand, ServiceResponse<FrontPageDto>>
    {
        private readonly IFrontPageRepository _FrontPageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontPageCommandHandler> _logger;
        public DeleteFrontPageCommandHandler(
           IFrontPageRepository FrontPageRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontPageCommandHandler> logger
            )
        {
            _FrontPageRepository = FrontPageRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontPageDto>> Handle(DeleteFrontPageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontPageRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            _FrontPageRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontPageDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontPageDto>(entity);
            return ServiceResponse<FrontPageDto>.ReturnResultWith204();
        }
    }
}
