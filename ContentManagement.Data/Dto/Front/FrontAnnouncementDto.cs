using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public class FrontAnnouncementDto:BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public int IsSlider { get; set; }
        public int IsNews { get; set; }
        public int IsAnnouncement { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public FrontGalleryDto FrontGallery { get; set; }
        public List<FrontAnnouncementRecordDto> FrontAnnouncementRecords { get; set; }
    }
}
