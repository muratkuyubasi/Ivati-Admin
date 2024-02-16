using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public class HacPeriod
    {
        public HacPeriod()
        {
            HacForms = new HashSet<HacForm>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
        public virtual ICollection<HacForm> HacForms { get; set; }
    }
}
