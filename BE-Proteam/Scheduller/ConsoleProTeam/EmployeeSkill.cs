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
    
    public partial class EmployeeSkill
    {
        public int EmpSkill_id { get; set; }
        public Nullable<int> Employee_id { get; set; }
        public Nullable<int> Skillset_id { get; set; }
    
        public virtual ResourceEmployee ResourceEmployee { get; set; }
        public virtual Skillset Skillset { get; set; }
    }
}
