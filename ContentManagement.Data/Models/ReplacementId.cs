using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Models
{
    public class ReplacementId
    {
        public Guid Id { get; set; }

        public int SubId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
