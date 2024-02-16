using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string BirthPlace { get; set; }

        public string  Nationalitiy { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public bool IsDisabled { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePhoto { get; set; }
        public string Provider { get; set; }
        public bool IsActive { get; set; }
        public string Linkedin { get; set; }
        //public List<UserAllowedIPDto> UserAllowedIPs { get; set; }
        //public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
        //public List<UserClaimDto> UserClaims { get; set; } = new List<UserClaimDto>();

    }

    public class UserInformationDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDay { get; set; }
        public int GenderId { get; set; }
        public string Nationality { get; set; }
        public bool isDualNationality { get; set; }

        public bool? IsDead { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? MemberTypeId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string BurialPlace { get; set; }

        public string PlaceOfDeath { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public virtual GenderDTO? Gender { get; set; }
    }

    public class DiedUserInformationDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentificationNumber { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public string PlaceOfDeath { get; set; }

        public string BurialPlace { get; set; }

        public virtual ICollection<NoteDTO> Notes { get; set; }
    }

    public class UserSimpleDTO
    {
        public Guid Id { get; set; }

        public int? FamilyId { get; set; }

        public string? PicturePath { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int? CityId { get; set; }

        public string CreationDate { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public Guid? RoleId { get; set; }

    }
    public class UserPaginationDTO
    {
        public List<UserSimpleDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
