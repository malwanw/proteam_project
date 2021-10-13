using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class ResourceEmployee
    {

        public ResourceEmployee()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Npp { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? LastWorkDate { get; set; }
        public decimal? TotalManhour { get; set; }
        public int? ResourceType { get; set; }
        public int? VendorId { get; set; }
        public int? JenjabId { get; set; }
        public int? DivisiId { get; set; }
        public int? KelompokId { get; set; }
        public int? Role { get; set; }
        public string ProjectExp { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual Jenjab Jenjab { get; set; }
        public virtual Kelompok Kelompok { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
