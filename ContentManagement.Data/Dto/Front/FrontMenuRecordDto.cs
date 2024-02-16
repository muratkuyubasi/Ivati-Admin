using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Data.Dto
{
    public class FrontMenuRecordDto
    {
        public int Id { get; set; }
        public int FrontMenuId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
        public virtual FrontMenuDto FrontMenu { get; set; }
    }
}
