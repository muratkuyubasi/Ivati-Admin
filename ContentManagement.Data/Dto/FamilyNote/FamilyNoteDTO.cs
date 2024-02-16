using ContentManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Data.Dto
{
    public class FamilyNoteDTO
    {
        public int Id { get; set; }
        public Guid FamilyId { get; set; }
        public string Text { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
