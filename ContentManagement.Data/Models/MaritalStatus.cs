using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            UmreForms = new HashSet<UmreForm>();
            HacForms = new HashSet<HacForm>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UmreForm> UmreForms { get; set; }
        public virtual ICollection<HacForm> HacForms { get; set; }

    }
}
