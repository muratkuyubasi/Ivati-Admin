using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Address
    {
        public Guid FamilyId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string District { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Family Family { get; set; }
    }
}
