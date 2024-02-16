using MediatR;
using CenazeFonu.Data.Resources;
using CenazeFonu.Repository;

namespace CenazeFonu.MediatR.Queries
{
    public class GetNLogsQuery : IRequest<NLogList>
    {
        public NLogResource NLogResource { get; set; }
    }
}
