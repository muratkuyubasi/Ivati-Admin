using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using Hafiz.Core.Utilities.Mail;

namespace ContentManagement.MediatR.Handlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, ServiceResponse<EmailDto>>
    {
        private readonly IEmailSMTPSettingRepository _emailSMTPSettingRepository;
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        public SendEmailCommandHandler(
           IEmailSMTPSettingRepository emailSMTPSettingRepository,
            ILogger<SendEmailCommandHandler> logger,
            UserInfoToken userInfoToken
            )
        {
            _emailSMTPSettingRepository = emailSMTPSettingRepository;
            _logger = logger;
            _userInfoToken = userInfoToken;
        }
        public async Task<ServiceResponse<EmailDto>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var defaultSmtp = await _emailSMTPSettingRepository.FindBy(c => c.IsDefault).FirstOrDefaultAsync();
            if (defaultSmtp == null)
            {
                _logger.LogError("Default SMTP setting does not exist.");
                return ServiceResponse<EmailDto>.Return404("Default SMTP setting does not exist.");
            }
            try
            {
                //EmailHelper.SendEmail(new SendEmailSpecification
                //{
                //    Body = request.Body,
                //    FromAddress = "noreply@ditib.it",
                //    Host = "smtps.aruba.it",
                //    IsEnableSSL = defaultSmtp.IsEnableSSL,
                //    Password = "DitibItalia@2024",
                //    Port = 465,
                //    Subject = request.Subject,
                //    ToAddress = request.ToAddress,
                //    CCAddress = request.CCAddress,
                //    UserName = "noreply@ditib.it",
                //    Attechments = request.Attechments,
                //    AttachmentFileURL = request.AttachmentFileURL,
                //    AttachmentRootPath = request.AttachmentRootPath
                //});
                EmailHelper.SendMailByMailKit(request.Body, request.Subject, request.ToAddress, request.CCAddress);
                //MailHelper.SendMail(request.Body, request.Subject, request.ToAddress); 
                return ServiceResponse<EmailDto>.ReturnSuccess();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ServiceResponse<EmailDto>.ReturnFailed(500, e.Message);
            }
        }
    }
}
