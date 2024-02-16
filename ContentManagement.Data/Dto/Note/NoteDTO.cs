using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool isDeleted { get; set; }

        //public virtual User User { get; set; }
    }
}
