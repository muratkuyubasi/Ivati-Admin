using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate, PTContext>,
          IEmailTemplateRepository
    {
        public EmailTemplateRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {

        }
    }
}

