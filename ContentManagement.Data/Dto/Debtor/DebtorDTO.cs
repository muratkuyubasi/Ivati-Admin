using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ContentManagement.Data.Dto
{
    public partial class DebtorDTO
    {
        public Guid Id { get; set; }
        public string DebtorNumber { get; set; }
        public Guid FamilyId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPayment { get; set; }

        public bool? IsDeleted { get; set; }
        public decimal Amount { get; set; }

        public int? DebtorTypeId { get; set; }

        public DateTime? PaymentDate { get; set; }
        [DefaultValue(typeof(DateTime), "2024-01-31")]
        public DateTime? DueDate { get; set; }

        public virtual DebtorTypeDTO DebtorType { get; set; }

        public virtual DebtorFDTO Family { get; set; }

        //public virtual FamilyInformationDTO Family { get; set; }
    }

    public class DebtorFamilyDTO
    {
        public Guid Id  { get; set; }

        public string DebtorNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPayment { get; set; }

        public bool? IsDeleted { get; set; }
        public decimal Amount { get; set; }

        public DateTime? PaymentDate { get; set; }
        [DefaultValue(typeof(DateTime), "2024-01-31")]
        public DateTime? DueDate { get; set; }

        public int DebtorTypeId { get; set; }

        public virtual DebtorTypeDTO DebtorType { get; set; }
    }

    public class DebtorByYearDTO
    {
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    public class DebtorPaginationDto
    {
        public List<DebtorSimpleDTO> Data { get; set; }
        public int Skip { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class DebtorSimpleDTO
    {
        public Guid Id { get; set; }
        public int MemberId { get; set; }
        public Guid FamilyId { get; set; }
        public bool IsPayment { get; set; }

        public string FullName { get; set; }
        public string DebtorNumber { get; set; }
        public string DebtorType { get; set; }
        public decimal Amount { get; set; }

        public string? CreationDate { get; set;}

        public string? DueDate { get; set; }

        public string? PaymentDate { get; set; }

        public int? CityId { get; set; }
    }
}
