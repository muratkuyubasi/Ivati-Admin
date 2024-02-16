﻿using Microsoft.AspNetCore.Identity;
using System;

namespace CenazeFonu.Data
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
