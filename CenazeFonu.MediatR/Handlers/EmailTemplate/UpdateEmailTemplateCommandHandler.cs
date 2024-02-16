using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;

namespace CenazeFonu.MediatR.Handlers
{
    public class UpdateEmailTemplateCommandHandler : IRequestHandler<UpdateEmailTemplateCommand, ServiceResponse<EmailTemplateDto>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateEmailTemplateCommandHandler> _logger;
        public UpdateEmailTemplateCommandHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            UserInfoToken userInfoToken,
            ILogger<UpdateEmailTemplateCommandHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<EmailTemplateDto>> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");
                return ServiceResponse<EmailTemplateDto>.Return409("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EmailTemplateDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);
            return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(entityDto);
        }
    }
}
