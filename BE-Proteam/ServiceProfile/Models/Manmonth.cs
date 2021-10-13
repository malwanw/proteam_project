using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Manmonth
    {
        public int ManmonthId { get; set; }
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? LastContract { get; set; }
        public int? TotalManmonth { get; set; }
        public int? UsageManmonth { get; set; }
        public int? AvailableManmonth { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Notes { get; set; }
    }
}
