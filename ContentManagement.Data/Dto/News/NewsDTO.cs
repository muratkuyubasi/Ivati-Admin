using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.DataDto
{
    public class NewsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
