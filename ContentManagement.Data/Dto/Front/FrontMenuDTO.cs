using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data.Dto
{
    public class FrontMenuDto : BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> FrontPageId { get; set; }
        public FrontPageDto FrontPage { get; set; }

        public FrontMenu Parent { get; set; }
        public List<FrontMenuRecordDto> FrontMenuRecords { get; set; }
        //public virtual ICollection<FrontMenu> SubMenus { get; set; }
        public FrontMenuRecord FrontMenuRecord { get; set; }
    }
}
