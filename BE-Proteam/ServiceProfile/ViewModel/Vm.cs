using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.ViewModel
{
    public class GetRE
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Npp { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? LastWorkDate { get; set; }
        public decimal? TotalManhour { get; set; }
        public decimal? Cost { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public int? ResourceType { get; set; }
        public string tipe_resource { get; set; }
        public int? JenjabId { get; set; }
        public string JenjangJabatan { get; set; }
        public int? DivisiId { get; set; }
        public string nama_divisi { get; set; }
        public int? KelompokId { get; set; }
        public string Kelompok { get; set; }
        public int? Role { get; set; }
        public string nama_role { get; set; }
        public string ProjectExp { get; set; }
        public int? Status { get; set; }
        public string nama_status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Skill { get; set; }
        public List<SkillName> ListSkill { get; set; }

    }
    public class SkillName
    {
        public int? Skillset_id { get; set; }
        public string Skillset { get; set; }
    }

    public class Skills
    {
       public string Skillset { get; set; }
    }

    public class ContohData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class VendorMandays
    {
        public int Mandays_id { get; set; }
        public string VendorName { get; set; }

        public int TotalMandays { get; set; }
    }

    public class DashboardVendor
    {
        public int TotalVendor { get; set; }

        public int TotalMandays { get; set; }
        public List<VendorMandays> ListVendors { get; set; }
    }

    public class ResourceKelompok
    {
        public int Kelompok_id { get; set; }
        public string Kelompok { get; set; }

        public int TotalResourceKelompok { get; set; }
    }

    public class ResourceRole
    {
        public int Role { get; set; }
        public string Name { get; set; }

        public int TotalResourceRole { get; set; }
    }

    public class ResourceType
    {
        public int ResourceTypeVal { get; set; }
        public string Name { get; set; }

        public int TotalResourceType { get; set; }
        public string PercentageRecourceType { get; set; }
    }


    public class DashboardResources
    {
        public int TotalResource { get; set; }

        public List<ResourceKelompok> ListResourceKelompok { get; set; }

        public List<ResourceRole> ListResourceRole { get; set; }

        public List<ResourceType> ListResourceType { get; set; }
    }

    public class MandaysStatus
    {
        public int MandaysId { get; set; }
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? LastContract { get; set; }
        public int TotalMandays { get; set; }
        public int UsageMandays { get; set; }
        public int AvailableMandays { get; set; }
        public int Status { get; set; }
        public string nama_status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Notes { get; set; }
    }

    public class GetManmonth
    {
        public int ManmonthId { get; set; }
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? LastContract { get; set; }
        public int TotalManmonth { get; set; }
        public int UsageManmonth { get; set; }
        public int AvailableManmonth { get; set; }
        public int Status { get; set; }
        public string nama_status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Notes { get; set; }
    }

    public class Member
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Npp { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? LastWorkDate { get; set; }
        public decimal? TotalManhour { get; set; }
        public decimal? Cost { get; set; }
        public int? ResourceType { get; set; }
        public string tipe_resource { get; set; }
        public int? JenjabId { get; set; }
        public string JenjangJabatan { get; set; }
        public int? KelompokId { get; set; }
        public string Kelompok { get; set; }
        public int? Role { get; set; }
        public string nama_role { get; set; }
        public string ProjectExp { get; set; }
        public int? Status { get; set; }
        public string nama_status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

    //public class VMKelompok
    //{
    //    public int KelompokId { get; set; }
    //    public string KelompokName { get; set; }
    //    public int? DivisiId { get; set; }
    //    public string DivisiName { get; set; }
    //    public int Status { get; set; }
    //    public string nama_status { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public DateTime? CreatedTime { get; set; }
    //    public DateTime? UpdateTime { get; set; }

    //}
    public class GetKelompok
    {
        public int KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int? DivisiId { get; set; }
        public string DivisiName { get; set; }
        public int Status { get; set; }
        public string nama_status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
    public class KelompokProfile
    {
        public int KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int? TotalEmployee { get; set; }
        public decimal? TotalManhour { get; set; }
        public int Status { get; set; }
        public string nama_status { get; set; }
        public List<Member> ListMember { get; set; }
        public string Skillset { get; set; }

    }

    public class UnitProfile
    {
        public int Unit_id { get; set; }
        public int? Kelompok_id { get; set; }
        public string KelompokName { get; set; }
        public int? Divisi_id { get; set; }
        public string DivisiName { get; set; }
        public int? TotalEmployee { get; set; }
        public decimal? TotalManhour { get; set; }
        public string Skill { get; set; }
        public int? Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public List<Member> ListMember { get; set; }

    }

    public class PostRE
    {
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
        public List<ListSkillEmp> ListSkill { get; set; }

    }
    public class ListSkillEmp
    {
        public int? SkillId { get; set; }
    }

    public class PostManday
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
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        
    }

    public class PostManmonth
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
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

    }
    public class PostUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Npp { get; set; }
        //public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int? Role { get; set; }
        public string RoleName { get; set; }
        public int? KelompokId { get; set; }
        public string Kelompok { get; set; }
        public int? Status { get; set; }
        public string StatusName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
    public class PostLookup
    {
        public int Lookup_id { get; set; }
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
    public class UpdatePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class PostKelompok
    {
        public int KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int? DivisiId { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

}

   