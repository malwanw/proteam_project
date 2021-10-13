using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Manday
    {
        public int MandaysId { get; set; }
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? LastContract { get; set; }
        public int? TotalMandays { get; set; }
        public int? UsageMandays { get; set; }
        public int? AvailableMandays { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Notes { get; set; }
    }
}
