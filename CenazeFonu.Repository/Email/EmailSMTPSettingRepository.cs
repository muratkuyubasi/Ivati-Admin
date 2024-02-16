using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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
