//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleProTeam
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manday
    {
        public int Mandays_id { get; set; }
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public Nullable<System.DateTime> StartContract { get; set; }
        public Nullable<System.DateTime> LastContract { get; set; }
        public Nullable<int> TotalMandays { get; set; }
        public Nullable<int> UsageMandays { get; set; }
        public Nullable<int> AvailableMandays { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string Notes { get; set; }
    }
}
