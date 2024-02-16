using Microsoft.AspNetCore.Identity;
using System;

namespace ContentManagement.Data
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
