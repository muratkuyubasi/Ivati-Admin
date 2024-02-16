using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Data.Dto
{
    public class FrontGalleryRecordDto
    {
        public int Id { get; set; }
        public int FrontGalleryId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
        public List<FrontGalleryMediaDto> FrontGalleryMedias { get; set; }

    }
}
