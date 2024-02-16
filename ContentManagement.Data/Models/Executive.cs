using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Executive
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
