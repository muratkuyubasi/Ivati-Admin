using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Resources;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class LoginAuditRepository : GenericRepository<LoginAudit, PTContext>,
       ILoginAuditRepository
    {
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly ILogger<LoginAuditRepository> _logger;
        private readonly IPropertyMappingService _propertyMappingService;
        public LoginAuditRepository(
            IUnitOfWork<PTContext> uow,
            ILogger<LoginAuditRepository> logger,
            IPropertyMappingService propertyMappingService
            ) : base(uow)
        {
            _uow = uow;
            _logger = logger;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(loginAuditResrouce.OrderBy,
               _propertyMappingService.GetPropertyMapping<LoginAuditDto, LoginAudit>());

            if (!string.IsNullOrWhiteSpace(loginAuditResrouce.UserName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.UserName, $"%{loginAuditResrouce.UserName}%"));
            }

            var loginAudits = new LoginAuditList();
            return await loginAudits.Create(
                collectionBeforePaging,
                loginAuditResrouce.Skip,
                loginAuditResrouce.PageSize
                );
        }

        public async Task LoginAudit(LoginAuditDto loginAudit)
        {
            try
            {
                Add(new LoginAudit
                {
                    Id = Guid.NewGuid(),
                    LoginTime = DateTime.Now.ToLocalTime(),
                    Provider = loginAudit.Provider,
                    Status = loginAudit.Status,
                    UserName = loginAudit.UserName,
                    RemoteIP = loginAudit.RemoteIP,
                    Latitude = loginAudit.Latitude,
                    Longitude = loginAudit.Longitude
                });
                //await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
