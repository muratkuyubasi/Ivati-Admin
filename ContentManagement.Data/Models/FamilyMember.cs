using System;
using System.Collections.Generic;
using System.Reflection;

namespace ContentManagement.Data.Models
{
    public partial class FamilyMember
    {
        public Guid Id { get; set; }
        public Guid FamilyId { get; set; }
        public Guid MemberUserId { get; set; }
        public int? MemberTypeId { get; set; }

        public bool? IsDivorced { get; set; }

        public virtual Family Family { get; set; }
        public virtual MemberType MemberType { get; set; }
        public virtual User MemberUser { get; set; }
    }
}
