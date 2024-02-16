using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Citizenship
    {
        public Citizenship()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
