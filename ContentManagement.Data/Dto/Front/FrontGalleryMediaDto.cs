using Microsoft.AspNetCore.Http;

namespace ContentManagement.Data.Dto
{
    public class FrontGalleryMediaDto : BaseEntity
    {
        public int Id { get; set; }
        public int FrontGalleryRecordId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
    }
}
