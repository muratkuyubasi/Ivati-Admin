using System.Collections.Generic;
using ContentManagement.Data.Dto;

namespace ContentManagement.Repository
{
    public interface IConnectionMappingRepository
    {
        bool AddUpdate(UserInfoToken tempUserInfo, string connectionId);
        void Remove(UserInfoToken tempUserInfo);
        IEnumerable<UserInfoToken> GetAllUsersExceptThis(UserInfoToken tempUserInfo);
        UserInfoToken GetUserInfo(UserInfoToken tempUserInfo);
        UserInfoToken GetUserInfoByName(string id);
        UserInfoToken GetUserInfoByConnectionId(string connectionId);

    }
}
