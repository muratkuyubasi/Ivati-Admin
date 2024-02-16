using MediatR;
using ContentManagement.Data.Resources;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Queries
{
    public class GetNLogsQuery : IRequest<NLogList>
    {
        public NLogResource NLogResource { get; set; }
    }
}
