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
    
    public partial class UnitProfiling
    {
        public int Unit_id { get; set; }
        public Nullable<int> Kelompok_id { get; set; }
        public Nullable<decimal> TotalEmployee { get; set; }
        public Nullable<decimal> TotalManhour { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        public string KelompokName { get; set; }
        public string Skill { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> Divisi_id { get; set; }
        public string DivisiName { get; set; }
    
        public virtual Kelompok Kelompok { get; set; }
    }
}
