using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class MemberType
    {
        public MemberType()
        {
            FamilyMembers = new HashSet<FamilyMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
    }
}
