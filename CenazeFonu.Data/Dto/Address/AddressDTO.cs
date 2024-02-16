using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CenazeFonu.Data.Models;

namespace CenazeFonu.Data.Dto
{

    public partial class AddressDTO
    {
        [Key]
        public Guid FamilyId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string District { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }


        //public virtual Family Family{ get; set; }
    }
}
