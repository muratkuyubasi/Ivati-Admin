using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public class FrontGalleryDto : BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
        public List<FrontGalleryRecordDto> FrontGalleryRecords { get; set; }
    }
}
