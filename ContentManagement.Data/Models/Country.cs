using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data
{
    public class Country
    {
        public Country() {
            Cities = new HashSet<City>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Langcode { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
