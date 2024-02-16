using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Family
    {
        public Family()
        {
            Debtors = new HashSet<Debtor>();
            FamilyMembers = new HashSet<FamilyMember>();
            Spouses = new HashSet<Spouse>();
            FamilyNotes = new HashSet<FamilyNote>();
        }

        public int MemberId { get; set; }
        public int? ReferenceNumber { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Debtor> Debtors { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<Spouse> Spouses { get; set; }

        public virtual ICollection<FamilyNote> FamilyNotes { get; set; }
    }
}
