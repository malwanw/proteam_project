using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class UnitProfiling
    {
        public int UnitId { get; set; }
        public int? KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int? DivisiId { get; set; }
        public string DivisiName { get; set; }
        public decimal? TotalEmployee { get; set; }
        public decimal? TotalManhour { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Skill { get; set; }
        public string StatusName { get; set; }
       
    }
}
