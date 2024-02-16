using ContentManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ContentManagement.Data.Dto
{
    public class FamilyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public int? ReferenceNumber { get; set; }
        public int MemberId { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<FamilyMemberDTO> FamilyMembers { get; set; }

        private int _memberCount;

        public int MemberCount
        {
            get
            {
                if (FamilyMembers.Count() < 1)
                {

                    return 0;
                }
                else return FamilyMembers.Count() - FamilyMembers.Where(x => x.MemberUser.IsDead == true || x.MemberUser.IsDeleted == true).Count();
            }
            set { _memberCount = FamilyMembers.Where(x => x.MemberUser.IsDeleted == false).Count(); }
        }

        private int _familyDebtorAmount;
        public int FamilyDebtorAmount
        {

            get
            {
                foreach (var d in Debtors)
                {
                    if (d.IsPayment == false)
                    {
                        _familyDebtorAmount += (int)d.Amount;
                    }
                }
                return _familyDebtorAmount;
            }

            set
            {
                foreach (var d in Debtors)
                {
                    if (d.IsPayment == false)
                    {
                        _familyDebtorAmount += (int)d.Amount;
                    }
                }
            }
        }

        public AddressDTO Address { get; set; }
        public List<DebtorDTO> Debtors { get; set; }

        public virtual ICollection<FamilyNoteDTO> FamilyNotes { get; set; }
    }

    public class FamilyPaginationDto
    {
        public List<FamilySimpleDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class DeletedFamiliesPaginationDTO
    {
        public List<DeletedFamiliesInformationDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class FamilyDebtorPaginationDto
    {
        public List<FamilyInformationDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class FamilyInformationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public AddressDTO Address { get; set; }

        public string _fullName;

        [IgnoreDataMember]
        public virtual ICollection<FamilyMemberDTO> FamilyMembers
        {
            get; set;
        }

        public string FullName
        {
            get
            {
                if (FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1) != null)
                {
                    _fullName = FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName;
                }
                return _fullName;
            }


            set
            {
                if (FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1) != null)
                { _fullName = FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName; }
            }
        }

        public virtual ICollection<DebtorFamilyDTO> Debtors { get; set; }
    }

    public class DeletedFamiliesInformationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public AddressDTO Address { get; set; }
    }

    public class DiedFamilyMembersFamilyInformationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public AddressDTO Address { get; set; }
    }

    public class DebtorFDTO
    {
        public Guid Id { get; set; }
        public int MemberId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public string _fullName;

        [IgnoreDataMember]
        public virtual ICollection<FamilyMemberDTO> FamilyMembers
        {
            get; set;
        }

        public string FullName
        {
            get
            {
                if (FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1) != null)
                {
                    _fullName = FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName;
                }
                return _fullName;
            }


            set
            {
                if (FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1) != null)
                { _fullName = FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + FamilyMembers.Where(x => x.MemberUser.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName; }
            }
        }
    }

    public class FamilySimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public int? ReferenceNumber { get; set; }
        public int MemberId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public string? CreationDate { get; set; }

        public string? Personnummer { get; set; }

        public int? GenderId { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Nationality { get; set; }

        public int? MemberCount { get; set; }

        public decimal? TotalDebtorAmount { get; set; }
    }
}
