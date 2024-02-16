using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class SpouseDTO
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        //public Guid HusbandId { get; set; }
        //public Guid FamilyId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? GenderId { get; set; }
        //public virtual Gender Gender { get; set; }
        //public virtual Family Family { get; set; }
        //public virtual User Husband { get; set; }

        public virtual GenderDTO? Gender { get; set; }
        public virtual UserInformationDTO? User { get; set; }
    }
}
