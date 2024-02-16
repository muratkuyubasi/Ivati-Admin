using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Data.Dto
{
    public class FrontAnnouncementRecordDto
    {
        public int Id { get; set; }
        public int FrontAnnouncementId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string FileUrl { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
