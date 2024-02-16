using ContentManagement.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }

        public string BirthPlace { get; set; }

        public string Nationality { get; set; }

        public string IdentificationNumber { get; set; }
        public int GenderId { get; set; }
        public bool IsDualNationality { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public bool? IsDead { get; set; }
        public string ProfilePhoto { get; set; }
        public string Provider { get; set; }
        public string Address { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public string PlaceOfDeath { get; set; } = null;

        public string BurialPlace { get; set; } = null;

        public int? MemberTypeId { get; set; }

        public int? CityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<UserAllowedIP> UserAllowedIPs { get; set; }

        public virtual Family Family { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<Spouse> Spouses { get; set; }
        public virtual Gender Gender { get; set; }
        //public virtual Address Address { get; set; }
        public virtual ICollection<Spouse> SpouseUsers { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Executive> Executives { get; set; }
    }
}
