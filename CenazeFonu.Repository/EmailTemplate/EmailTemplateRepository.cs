using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
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

