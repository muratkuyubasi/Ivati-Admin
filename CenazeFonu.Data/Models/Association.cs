using System;
using System.Collections.Generic;

namespace CenazeFonu.Data.Models
{
    public partial class Association
    {
        public Association()
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
