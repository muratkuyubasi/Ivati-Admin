using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? CreationDate { get; set; }
        public string LinkImage { get; set; }
        public string LinkUrl { get; set; }
    }
}
