using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ContentManagement.Data.Models
{
    public partial class Debtor
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

        public virtual Family Family { get; set; }

        public virtual DebtorType DebtorType { get; set; }
    }
}
