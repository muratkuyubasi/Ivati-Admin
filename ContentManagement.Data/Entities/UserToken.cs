using Microsoft.AspNetCore.Identity;
using ContentManagement.Data;
using System;

namespace ContentManagement.Data
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; } = null;
    }
}
