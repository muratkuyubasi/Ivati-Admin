using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using System.Threading.Tasks;
using CenazeFonu.Data.Resources;
using System.Collections.Generic;
using System;

namespace CenazeFonu.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);

        //Task<User> Login(string username, string password);
    }
}
