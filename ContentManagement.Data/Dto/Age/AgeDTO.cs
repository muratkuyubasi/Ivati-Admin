using System;
using System.Collections.Generic;

namespace ContentManagement.Data.Dto
{
    public partial class AgeDTO
    {
        public int Id { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; }
        public decimal Amount { get; set; }

        public decimal? EntranceFree { get; set; }

        public decimal? Dues { get; set; }
    }
}
