using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data
{
    public class FrontAnnouncement:BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public int IsSlider { get; set; }
        public int IsNews { get; set; }
        public int IsAnnouncement { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        [ForeignKey("FrontGalleryId")]
        public virtual FrontGallery FrontGallery { get; set; }
        public virtual ICollection<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }
    }
}
