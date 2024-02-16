using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public class FamilyMemberDTO
    {
        public Guid Id { get; set; }

        public Guid MemberUserId { get; set; }
        public UserInformationDTO MemberUser { get; set; }
        //public FamilyInformationDTO Family { get; set; }
    }

    public class FamilyMemberWithFamilyDTO
    {
        public Guid Id { get; set; }

        public Guid MemberUserId { get; set; }
        public UserInformationDTO MemberUser { get; set; }

        public FamilyInformationDTO Family { get; set; }
    }

    public class DeletedFamilyMemberWithFamilyDTO
    {
        public Guid Id { get; set; }
        public Guid MemberUserId { get; set; }
        public int MemberId { get; set; }
        public string? FullName { get; set; }
        public string? Personummer { get; set; }
        public int GenderId { get; set; }
        public string? BirthDate { get; set; }
        public string? CaddeVeSokak { get; set; }
        public string? Il { get; set; }
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class DiedFamilyMemberDTO
    {
        public Guid Id { get; set; }

        public int FamilyId { get; set; }

        public Guid MemberUserId { get; set; }

        public string? FullName { get; set; }

        public string? PersonNo { get; set; }

        public string? DateOfDeath { get; set; }

        public string? PlaceOfDeath { get; set; }

        public string? BurialPlace { get; set; }

        public bool? IsDead { get; set; }
    }

    public class DiedFamilyMemberPaginationDTO
    {
        public List<DiedFamilyMemberDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class FamilyMemberWithFamilyPaginationDTO
    {
        public List<FamilyMemberWithFamilyDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class DeletedFamilyMemberWithFamilyPaginationDTO
    {
        public List<DeletedFamilyMemberWithFamilyDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
    public class FamilyMemberSimpleDTO
    {
        public Guid Id { get; set; }
        public int? FamilyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int MemberAge { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MemberTypeId { get; set; }
        public string? BirthDate { get; set; }
        public string? HouseHolderName { get; set; }
        public string? BirthPlace { get; set; }
        public int? GenderId { get; set; }
        public int? CityId { get; set; }

    }
    public class FamilyMemberPaginationDto
    {
        public List<FamilyMemberSimpleDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
