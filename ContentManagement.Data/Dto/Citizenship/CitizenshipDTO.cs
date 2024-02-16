using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public partial class CitizenshipDTO
    {
        public CitizenshipDTO()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
