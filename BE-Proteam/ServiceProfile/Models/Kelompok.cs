using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Kelompok
    {
        public Kelompok()
        {
            ResourceEmployees = new HashSet<ResourceEmployee>();
            UnitProfilings = new HashSet<UnitProfiling>();
        }

        public int KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int? DivisiId { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<ResourceEmployee> ResourceEmployees { get; set; }
        public virtual ICollection<UnitProfiling> UnitProfilings { get; set; }
    }
}
