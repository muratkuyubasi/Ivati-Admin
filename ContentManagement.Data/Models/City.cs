using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data
{
    public class City
    {
        public City()
        {
            Executives = new HashSet<Executive>();
            Families = new HashSet<Family>();
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Executive> Executives { get; set; }
        public virtual ICollection<Family> Families { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
