using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteEmailSMTPSettingCommandHandler : IRequestHandler<DeleteEmailSMTPSettingCommand, ServiceResponse<EmailSMTPSettingDto>>
    {
        private readonly IEmailSMTPSettingRepository _emailSMTPSettingRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly ILogger<DeleteEmailSMTPSettingCommandHandler> _logger;
        public DeleteEmailSMTPSettingCommandHandler(
            IEmailSMTPSettingRepository emailSMTPSettingRepository,
            IUnitOfWork<PTContext> uow,
            ILogger<DeleteEmailSMTPSettingCommandHandler> logger
            )
        {
            _emailSMTPSettingRepository = emailSMTPSettingRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(DeleteEmailSMTPSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailSMTPSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<EmailSMTPSettingDto>.Return404();
            }
            if (entityExist.IsDefault)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return422("You can not delete default Setting.");
            }
            entityExist.IsDeleted = true;
            _emailSMTPSettingRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith204();
        }
    }
}
