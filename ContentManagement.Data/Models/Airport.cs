using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Airport
    {
        public Airport()
        {
            HacFormDepartureAirports = new HashSet<HacForm>();
            HacFormLandingAirports = new HashSet<HacForm>();
            UmreFormDepartureAirports = new HashSet<UmreForm>();
            UmreFormLandingAirports = new HashSet<UmreForm>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HacForm> HacFormDepartureAirports { get; set; }
        public virtual ICollection<HacForm> HacFormLandingAirports { get; set; }
        public virtual ICollection<UmreForm> UmreFormDepartureAirports { get; set; }
        public virtual ICollection<UmreForm> UmreFormLandingAirports { get; set; }
    }
}
