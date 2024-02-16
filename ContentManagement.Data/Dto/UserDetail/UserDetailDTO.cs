using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public partial class UserDetailDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CitizenShipId { get; set; }
        public string IdentificationNumber { get; set; }

        //public virtual Citizenship CitizenShip { get; set; }
        //public virtual User User { get; set; }
    }
}
