using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public class FrontGallery:BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<FrontGalleryRecord> FrontGalleryRecords { get; set; }
    }
}
