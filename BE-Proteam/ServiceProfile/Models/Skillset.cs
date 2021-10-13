using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class Skillset
    {
        public Skillset()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        public int SkillsetId { get; set; }
        public string Skillset1 { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
