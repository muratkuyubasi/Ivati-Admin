using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContentManagement.Data.Models
{
    public partial class UserModel
    {
        public UserModel()
        {
            Families = new HashSet<FamilyDTO>();
            FamilyMembers = new HashSet<UserModelDTO>();
            UserDetails = new HashSet<UserDetailDTO>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public string BirthPlace { get; set; }

        public string Nationality { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public int? MemberTypeId { get; set; }

        public bool? IsDead { get; set; }
        public string ProfilePhoto { get; set; }
        public string Provider { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? BirthDay { get; set; }
        public int GenderId { get; set; }
        public string? PdfPath { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<FamilyDTO> Families { get; set; }
        public virtual ICollection<UserModelDTO> FamilyMembers { get; set; }
        public virtual ICollection<UserDetailDTO> UserDetails { get; set; }

        public virtual AddressDTO AddressDTO { get; set; }

        public virtual SpouseDTO Spouse{ get; set; }
    }
}
