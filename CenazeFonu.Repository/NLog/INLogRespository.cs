using System.Threading.Tasks;
using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Data;
using CenazeFonu.Data.Resources;

namespace CenazeFonu.Repository
{
    public interface INLogRespository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
