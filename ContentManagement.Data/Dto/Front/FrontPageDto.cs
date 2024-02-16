using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data.Dto
{
    public class FrontPageDto : BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public FrontGalleryDto FrontGallery { get; set; }
        public List<FrontPageRecordDto> FrontPageRecords { get; set; }
    }
}
