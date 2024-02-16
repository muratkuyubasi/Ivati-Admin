using System.Threading.Tasks;
using ContentManagement.Common.GenericRespository;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Resources;

namespace ContentManagement.Repository
{
    public interface ILoginAuditRepository : IGenericRepository<LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
