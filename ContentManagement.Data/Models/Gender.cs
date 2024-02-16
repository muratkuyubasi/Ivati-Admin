using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Models
{
    public partial class Gender
    {
        public Gender()
        {
            UmreForms = new HashSet<UmreForm>();
            HacForms = new HashSet<HacForm>();
            UserModels = new HashSet<UserModel>();
            Users = new HashSet<User>();
            Spouse = new HashSet<Spouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UmreForm> UmreForms { get; set; }

        public virtual ICollection<HacForm> HacForms { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Spouse> Spouse { get; set; }
    }
}
