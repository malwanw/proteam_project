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
    
    public partial class Kelompok
    {
        public Kelompok()
        {
            this.ResourceEmployees = new HashSet<ResourceEmployee>();
            this.UnitProfilings = new HashSet<UnitProfiling>();
        }
    
        public int Kelompok_id { get; set; }
        public string Kelompok1 { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> DivisiId { get; set; }
    
        public virtual ICollection<ResourceEmployee> ResourceEmployees { get; set; }
        public virtual ICollection<UnitProfiling> UnitProfilings { get; set; }
    }
}
