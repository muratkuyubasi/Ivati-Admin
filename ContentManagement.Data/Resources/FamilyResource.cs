using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Resources
{
    // ResourceParameter classı içerisinde sayfalamalarla ilgili propertyler var
    public class FamilyResource : ResourceParameter
    {
        public FamilyResource() : base("MemberId") // ResourceParameter içerisindeki OrderBy alanına bir property verdik, bu alana göre sıralama yapacak
        { }
        //public int MemberId { get; set; }
        //public int? ReferenceNumber { get; set; }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public Guid UserId { get; set; }
        //public bool? IsDeleted { get; set; }
        //public bool? IsActive { get; set; }
        ////public virtual User User { get; set; }
        //public virtual Address Address { get; set; }
        //public virtual ICollection<Debtor> Debtors { get; set; }
        //public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        //public virtual ICollection<FamilyNote> FamilyNotes { get; set; }
    }
}
