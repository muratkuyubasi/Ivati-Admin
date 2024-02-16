using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data
{
    public class FrontPage:BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        [ForeignKey("FrontGalleryId")]
        public virtual FrontGallery FrontGallery { get; set; }
        public virtual ICollection<FrontPageRecord> FrontPageRecords { get; set; }
    }
}
