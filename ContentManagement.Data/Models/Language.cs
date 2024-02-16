using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public partial class Language
    {
        public Language()
        {
            Translations = new HashSet<Translation>();
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Langcode { get; set; }
        public string? Flag { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Translation> Translations { get; set; }
    }
}
