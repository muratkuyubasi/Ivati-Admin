using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class CityDTO
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set;}

        public DateTime? CreationDate { get; set; }
    }

    public class CitySimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
