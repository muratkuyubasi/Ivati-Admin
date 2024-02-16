using System;
using System.ComponentModel.DataAnnotations;

namespace ContentManagement.Data.Dto
{
    public class FrontPageRecordDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int FrontPageId { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }
        public string PageContent { get; set; }
        public string LanguageCode { get; set; }
        public string FileUrl { get; set; }
    }
}
