using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class EmailSMTPSettingRepository : GenericRepository<EmailSMTPSetting, PTContext>,
           IEmailSMTPSettingRepository
    {
        public EmailSMTPSettingRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}
