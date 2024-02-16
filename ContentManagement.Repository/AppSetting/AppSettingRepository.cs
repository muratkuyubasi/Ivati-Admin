using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;

namespace ContentManagement.Repository
{
    public class AppSettingRepository : GenericRepository<AppSetting, PTContext>,
          IAppSettingRepository
    {
        public AppSettingRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {

        }
    }
}
