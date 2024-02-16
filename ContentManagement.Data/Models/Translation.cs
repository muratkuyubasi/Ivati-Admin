using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public partial class Translation
    {
        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string? LinkImage { get; set; }
        public string? LinkUrl { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Order { get; set; }
        public virtual Language Language { get; set; }
    }
}
