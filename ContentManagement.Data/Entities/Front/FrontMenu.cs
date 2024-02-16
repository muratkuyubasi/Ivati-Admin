using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data
{
    public class FrontMenu:BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual FrontMenu Parent { get; set; }

        //public virtual ICollection<FrontMenu> SubMenus { get; set; }
        public Nullable<int> FrontPageId { get; set; }
        [ForeignKey("FrontPageId")]
        public virtual FrontPage FrontPage { get; set; }
        public virtual ICollection<FrontMenuRecord> FrontMenuRecords { get; set; }
    }
}
