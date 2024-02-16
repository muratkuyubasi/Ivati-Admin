using ContentManagement.Common.GenericRespository;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using System.Threading.Tasks;
using ContentManagement.Data.Resources;
using System.Collections.Generic;
using System;

namespace ContentManagement.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);

        //Task<User> Login(string username, string password);
    }
}
