using System.Threading.Tasks;
using ContentManagement.Common.GenericRespository;
using ContentManagement.Data;
using ContentManagement.Data.Resources;

namespace ContentManagement.Repository
{
    public interface INLogRespository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
