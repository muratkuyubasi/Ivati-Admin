using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Data
{
    public class FrontMenuRecord
    {
        public int Id { get; set; }
        public int FrontMenuId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
        public virtual FrontMenu FrontMenu { get; set; }
    }
}
