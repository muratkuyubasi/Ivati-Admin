using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using Microsoft.Extensions.Logging;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteActionCommandHandler : IRequestHandler<DeleteActionCommand, ServiceResponse<ActionDto>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly ILogger<DeleteActionCommandHandler> _logger;
        public DeleteActionCommandHandler(
           IActionRepository actionRepository,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteActionCommandHandler> logger
            )
        {
            _actionRepository = actionRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<ActionDto>> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<ActionDto>.Return404();
            }

            _actionRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }

            return ServiceResponse<ActionDto>.ReturnSuccess();
        }
    }
}
