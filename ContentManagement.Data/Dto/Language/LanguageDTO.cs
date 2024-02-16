using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class LanguageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Langcode { get; set; }
        public string? Flag { get; set; }
    }
}
