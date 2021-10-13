using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Jenjab
    {
        public Jenjab()
        {
            ResourceEmployees = new HashSet<ResourceEmployee>();
        }

        public int JenjabId { get; set; }
        public string JenjangJabatan { get; set; }
        public decimal? Cost { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<ResourceEmployee> ResourceEmployees { get; set; }
    }
}
