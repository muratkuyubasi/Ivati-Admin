using MediatR;
using CenazeFonu.Data.Resources;
using CenazeFonu.Repository;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllLoginAuditQuery : IRequest<LoginAuditList>
    {
        public LoginAuditResource LoginAuditResource { get; set; }
    }
}
