using Microsoft.AspNetCore.Identity;
using CenazeFonu.Data;
using System;

namespace CenazeFonu.Data
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; } = null;
    }
}
