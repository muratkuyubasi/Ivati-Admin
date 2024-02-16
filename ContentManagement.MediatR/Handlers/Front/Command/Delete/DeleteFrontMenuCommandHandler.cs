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
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Repository;
using ContentManagement.Domain;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteFrontMenuCommandHandler : IRequestHandler<DeleteFrontMenuCommand, ServiceResponse<FrontMenuDto>>
    {
        private readonly IFrontMenuRepository _FrontMenuRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFrontMenuCommandHandler> _logger;
        public DeleteFrontMenuCommandHandler(
           IFrontMenuRepository FrontMenuRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteFrontMenuCommandHandler> logger
            )
        {
            _FrontMenuRepository = FrontMenuRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontMenuDto>> Handle(DeleteFrontMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = await _FrontMenuRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            _FrontMenuRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontMenuDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontMenuDto>(entity);
            return ServiceResponse<FrontMenuDto>.ReturnResultWith200(_mapper.Map<FrontMenuDto>(entityDto));
        }
    }
}
