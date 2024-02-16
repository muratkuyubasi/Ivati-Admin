using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public class UmrePeriod
    {
        public UmrePeriod()
        {
            UmreForms = new HashSet<UmreForm>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public virtual ICollection<UmreForm> UmreForms { get; set; }
    }
}
