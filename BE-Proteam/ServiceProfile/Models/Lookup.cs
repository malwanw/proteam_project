using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Lookup
    {
        public int LookupId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int? OrderNumber { get; set; }
        public int? Value { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
