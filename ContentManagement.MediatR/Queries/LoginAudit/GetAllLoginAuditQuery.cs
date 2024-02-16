using MediatR;
using ContentManagement.Data.Resources;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllLoginAuditQuery : IRequest<LoginAuditList>
    {
        public LoginAuditResource LoginAuditResource { get; set; }
    }
}
