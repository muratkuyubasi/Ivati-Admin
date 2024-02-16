using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Data.Models
{
    public partial class Age
    {
        public int Id { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; }
        public decimal Amount { get; set; }

        public decimal? EntranceFree { get; set; }

        public decimal? Dues { get; set; }
    }
}
