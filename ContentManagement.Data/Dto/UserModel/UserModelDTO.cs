using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ContentManagement.Data.Dto
{
    public partial class UserModelDTO
    {
        public UserModelDTO()
        {
            //Families = new HashSet<Family>();
            //FamilyMembers = new HashSet<FamilyMember>();
            //UserDetails = new HashSet<UserDetail>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public string BirthPlace { get; set; }

        public DateTime? BirthDay { get; set; }
        public int GenderId { get; set; }

        public string Nationality { get; set; }

        public bool IsDualNationality { get; set; }

        public bool? IsDead { get; set; }

        public int MemberTypeId { get; set; }

        public string? PdfPath { get; set; }
        //public virtual int? MemberId { get; set; }
        //[IgnoreDataMember]
        //public virtual string? FamilyName { get; set; }
        //[IgnoreDataMember]
        //public virtual string? PdfPath { get; set; }

        //public virtual ICollection<UserModelDTO> FamilyMembers { get; set; }

        //public virtual AddressDTO AddressDTO { get; set; }

        //public virtual SpouseDTO Spouse { get; set; }

        //public virtual Family Families { get; set; }
        //public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        //public virtual UserDetail UserDetails { get; set; }
    }

    public class UpdateUserContactDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PostCode { get; set; }
        public string District { get; set; }

        public string Street { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
    //public bool IsDeleted { get; set; }
    //public bool IsActive { get; set; }
    //public string ProfilePhoto { get; set; }
    //public string Provider { get; set; }
    //public string Address { get; set; }
    //public DateTime CreatedDate { get; set; }
    //public Guid? CreatedBy { get; set; }
    //public DateTime ModifiedDate { get; set; }
    //public Guid? ModifiedBy { get; set; }
    //public DateTime? DeletedDate { get; set; }
    //public Guid? DeletedBy { get; set; }
    //public string UserName { get; set; }
    //public string NormalizedUserName { get; set; }
    //public string Email { get; set; }
    //public string NormalizedEmail { get; set; }
    //public bool EmailConfirmed { get; set; }
    //public string PasswordHash { get; set; }
    //public string SecurityStamp { get; set; }
    //public string ConcurrencyStamp { get; set; }
    //public string PhoneNumber { get; set; }
    //public bool PhoneNumberConfirmed { get; set; }
    //public bool TwoFactorEnabled { get; set; }
    //public DateTimeOffset? LockoutEnd { get; set; }
    //public bool LockoutEnabled { get; set; }
    //public int AccessFailedCount { get; set; }
}
