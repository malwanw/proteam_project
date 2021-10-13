using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceProfile.Component;
using ServiceProfile.Data;
using ServiceProfile.Models;
using ServiceProfile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceEmployeesController : ControllerBase
    {
        private IManmonth _manmonth;
        private IResourceEmployee _resourceemployee;
        public ProteamContext _context;

        public ResourceEmployeesController(IManmonth manmonth, IResourceEmployee resourceemployee, ProteamContext context)
        {
            _context = context;
            _resourceemployee = resourceemployee;
            _manmonth = manmonth;
        }
   
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                List<GetRE> data = new List<GetRE>();
                List<GetRE> employee = new List<GetRE>();
                List <SkillName> skillname = new List<SkillName>();


                data = StoredProcedureExecutor.ExecuteSPList<GetRE>(_context, "sp_resource_employee", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (GetRE Data2 in data)
                {
                    GetRE NewData = new GetRE();
                    NewData.EmployeeId = Data2.EmployeeId;
                    NewData.EmployeeName = Data2.EmployeeName;
                    NewData.Npp = Data2.Npp;
                    NewData.Email = Data2.Email;
                    NewData.Phone = Data2.Phone;
                    NewData.ActiveDate = Data2.ActiveDate;
                    NewData.LastWorkDate = Data2.LastWorkDate;
                    NewData.TotalManhour = Data2.TotalManhour;
                    NewData.Cost = Data2.Cost;
                    NewData.VendorId = Data2.VendorId;
                    NewData.VendorName = Data2.VendorName;
                    NewData.ResourceType = Data2.ResourceType;
                    NewData.tipe_resource = Data2.tipe_resource;
                    NewData.Role = Data2.Role;
                    NewData.nama_role = Data2.nama_role;
                    NewData.DivisiId = Data2.DivisiId;
                    NewData.nama_divisi = Data2.nama_divisi;
                    NewData.Kelompok = Data2.Kelompok;
                    NewData.JenjangJabatan = Data2.JenjangJabatan;
                    NewData.ProjectExp = Data2.ProjectExp;
                    NewData.Status = Data2.Status;
                    NewData.CreatedBy = Data2.CreatedBy;
                    NewData.UpdatedBy = Data2.UpdatedBy;
                    NewData.CreatedTime = Data2.CreatedTime;
                    NewData.UpdateTime = Data2.UpdateTime;
                    NewData.JenjabId = Data2.JenjabId;
                    NewData.KelompokId = Data2.KelompokId;
                    NewData.nama_status = Data2.nama_status;

                    skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "sp_resource_employee", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", NewData.EmployeeId),
                        new SqlParameter("@Parameter2", "Test")
                    });
                    if (skillname.Count() > 0)
                    {
                        string DataSkill = "";
                        foreach (SkillName s in skillname)
                        {
                            DataSkill = DataSkill + " " + s.Skillset + "<br/>";
                        }
                    
                        NewData.Skill = DataSkill;
                    }
                    NewData.ListSkill = skillname;
                    employee.Add(NewData);
                    
                }


                return Ok(new { employee });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
        }
        
        // GET: Home  
        [HttpGet("ExportToExcel")]
        public IActionResult ExportDataToExcel()
        {
            //Creating DataTable  
            List<GetRE> data = new List<GetRE>();
            List<SkillName> skillname = new List<SkillName>();
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "ResourceProfile";
            //Add Columns  
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("NPP", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Active Date", typeof(DateTime));
            dt.Columns.Add("Last Working Date", typeof(DateTime));
            dt.Columns.Add("Divisi", typeof(string));
            dt.Columns.Add("Kelompok", typeof(string));
            dt.Columns.Add("Resource Type", typeof(string));
            dt.Columns.Add("Jenjab", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            dt.Columns.Add("Skillset", typeof(string));
            dt.Columns.Add("Manhour/Day", typeof(int));
            dt.Columns.Add("Pricing", typeof(string));
            data = StoredProcedureExecutor.ExecuteSPList<GetRE>(_context, "sp_resource_employee", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
             });
           
            foreach (GetRE d in data)
            {
                skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "sp_resource_employee", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", d.EmployeeId),
                        new SqlParameter("@Parameter2", "Test")
                });
                List<string> skillset = new List<string>();
                //var skills = String.Join(", ", skillname);
                 //string DataSkill = "";
                 foreach (SkillName s in skillname)
                  {
                    skillset.Add(s.Skillset);
                  }
                var dataSkills = String.Join(", ", skillset);
                //convert price to rupiah
                var rupiahCost = String.Format(CultureInfo.CreateSpecificCulture("id-ID"), "Rp. {0:N}", Convert.ToInt32(d.Cost));

                //Add Rows in DataTable  
                dt.Rows.Add(
                    d.EmployeeName,
                    d.Npp, 
                    d.Email, 
                    d.Phone,
                    d.ActiveDate,
                    d.LastWorkDate,
                    d.nama_divisi,
                    d.Kelompok,
                    d.tipe_resource,
                    d.JenjangJabatan,
                    d.nama_role,
                    dataSkills,
                    d.TotalManhour,
                    rupiahCost
                    );
            }
            dt.AcceptChanges();
            //Name of File  
            string fileName = "ResourceProfile.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        // GET api/<ResourceEmployeesController>/5
        //    [HttpGet("{id}")]
        //public async Task<ResourceEmployee> Get(int id)
        //{
        //    var result = await _resourceemployee.GetById(id.ToString());
        //    return result;
        //}

        [HttpGet("GetByEmployeeId/{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                List<GetRE> data = new List<GetRE>();
                List<GetRE> employee = new List<GetRE>();
                List<SkillName> skillname = new List<SkillName>();


                data = StoredProcedureExecutor.ExecuteSPList<GetRE>(_context, "sp_resource_employee_byid", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (GetRE Data2 in data)
                {
                    GetRE NewData = new GetRE();
                    NewData.EmployeeId = Data2.EmployeeId;
                    NewData.EmployeeName = Data2.EmployeeName;
                    NewData.Npp = Data2.Npp;
                    NewData.Email = Data2.Email;
                    NewData.Phone = Data2.Phone;
                    NewData.ActiveDate = Data2.ActiveDate;
                    NewData.LastWorkDate = Data2.LastWorkDate;
                    NewData.TotalManhour = Data2.TotalManhour;
                    NewData.Cost = Data2.Cost;
                    NewData.VendorId = Data2.VendorId;
                    NewData.VendorName = Data2.VendorName;
                    NewData.ResourceType = Data2.ResourceType;
                    NewData.tipe_resource = Data2.tipe_resource;
                    NewData.Role = Data2.Role;
                    NewData.nama_role = Data2.nama_role;
                    NewData.DivisiId = Data2.DivisiId;
                    NewData.nama_divisi = Data2.nama_divisi;
                    NewData.Kelompok = Data2.Kelompok;
                    NewData.JenjangJabatan = Data2.JenjangJabatan;
                    NewData.ProjectExp = Data2.ProjectExp;
                    NewData.Status = Data2.Status;
                    NewData.CreatedBy = Data2.CreatedBy;
                    NewData.UpdatedBy = Data2.UpdatedBy;
                    NewData.CreatedTime = Data2.CreatedTime;
                    NewData.UpdateTime = Data2.UpdateTime;
                    NewData.JenjabId = Data2.JenjabId;
                    NewData.KelompokId = Data2.KelompokId;
                    NewData.nama_status = Data2.nama_status;

                    skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "sp_resource_employee_byid", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                    });
                    if (skillname.Count() > 0)
                    {
                        string DataSkill = "";
                        foreach (SkillName s in skillname)
                        {
                            DataSkill = DataSkill + " " + s.Skillset + "<br/>";
                        }
                        NewData.Skill = DataSkill;
                    }
                    NewData.ListSkill = skillname;
                    employee.Add(NewData);

                }


                return Ok(new { employee });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
        }

        [HttpGet("GetByKelompokId/{id}")]
        public IActionResult GetByKelompokId(int id)
        {

            try
            {
                List<GetRE> data = new List<GetRE>();
                List<GetRE> employee = new List<GetRE>();
                List<SkillName> skillname = new List<SkillName>();


                data = StoredProcedureExecutor.ExecuteSPList<GetRE>(_context, "sp_resource_employee_bykelompok", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (GetRE Data2 in data)
                {
                    GetRE NewData = new GetRE();
                    NewData.EmployeeId = Data2.EmployeeId;
                    NewData.EmployeeName = Data2.EmployeeName;
                    NewData.Npp = Data2.Npp;
                    NewData.Email = Data2.Email;
                    NewData.Phone = Data2.Phone;
                    NewData.ActiveDate = Data2.ActiveDate;
                    NewData.LastWorkDate = Data2.LastWorkDate;
                    NewData.TotalManhour = Data2.TotalManhour;
                    NewData.Cost = Data2.Cost;
                    NewData.VendorId = Data2.VendorId;
                    NewData.VendorName = Data2.VendorName;
                    NewData.ResourceType = Data2.ResourceType;
                    NewData.tipe_resource = Data2.tipe_resource;
                    NewData.Role = Data2.Role;
                    NewData.nama_role = Data2.nama_role;
                    NewData.DivisiId = Data2.DivisiId;
                    NewData.nama_divisi = Data2.nama_divisi;
                    NewData.Kelompok = Data2.Kelompok;
                    NewData.JenjangJabatan = Data2.JenjangJabatan;
                    NewData.ProjectExp = Data2.ProjectExp;
                    NewData.Status = Data2.Status;
                    NewData.CreatedBy = Data2.CreatedBy;
                    NewData.UpdatedBy = Data2.UpdatedBy;
                    NewData.CreatedTime = Data2.CreatedTime;
                    NewData.UpdateTime = Data2.UpdateTime;
                    NewData.JenjabId = Data2.JenjabId;
                    NewData.KelompokId = Data2.KelompokId;
                    NewData.nama_status = Data2.nama_status;

                    skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "sp_resource_employee_bykelompok", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                    });
                    if (skillname.Count() > 0)
                    {
                        string DataSkill = "";
                        foreach (SkillName s in skillname)
                        {
                            DataSkill = DataSkill + " " + s.Skillset + "<br/>";
                        }
                        NewData.Skill = DataSkill;
                    }
                    NewData.ListSkill = skillname;
                    employee.Add(NewData);

                }


                return Ok(new { employee });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
        }

        [HttpGet("GetResourceByNpp/{npp}")]
        public IActionResult GetByNpp(string npp)
        {

            try
            {
                List<GetRE> data = new List<GetRE>();
                List<GetRE> employee = new List<GetRE>();
                List<SkillName> skillname = new List<SkillName>();


                data = StoredProcedureExecutor.ExecuteSPList<GetRE>(_context, "sp_resource_employee_bynpp", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", npp),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (GetRE Data2 in data)
                {
                    GetRE NewData = new GetRE();
                    NewData.EmployeeId = Data2.EmployeeId;
                    NewData.EmployeeName = Data2.EmployeeName;
                    NewData.Npp = Data2.Npp;
                    NewData.Email = Data2.Email;
                    NewData.Phone = Data2.Phone;
                    NewData.ActiveDate = Data2.ActiveDate;
                    NewData.LastWorkDate = Data2.LastWorkDate;
                    NewData.TotalManhour = Data2.TotalManhour;
                    NewData.Cost = Data2.Cost;
                    NewData.tipe_resource = Data2.tipe_resource;
                    NewData.nama_role = Data2.nama_role;
                    NewData.Kelompok = Data2.Kelompok;
                    NewData.JenjangJabatan = Data2.JenjangJabatan;
                    NewData.ProjectExp = Data2.ProjectExp;
                    NewData.Status = Data2.Status;
                    NewData.CreatedBy = Data2.CreatedBy;
                    NewData.UpdatedBy = Data2.UpdatedBy;
                    NewData.CreatedTime = Data2.CreatedTime;
                    NewData.UpdateTime = Data2.UpdateTime;
                    NewData.ResourceType = Data2.ResourceType;
                    NewData.Role = Data2.Role;
                    NewData.JenjabId = Data2.JenjabId;
                    NewData.KelompokId = Data2.KelompokId;
                    NewData.nama_status = Data2.nama_status;

                    skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "sp_resource_employee_bynpp", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", Data2.EmployeeId),
                        new SqlParameter("@Parameter2", "Test")
                    });
                    if (skillname.Count() > 0)
                    {
                        string DataSkill = "";
                        foreach (SkillName s in skillname)
                        {
                            DataSkill = DataSkill + " " + s.Skillset + "<br/>";
                        }
                        NewData.Skill = DataSkill;
                    }
                    NewData.ListSkill = skillname;
                    employee.Add(NewData);

                }


                return Ok(new { employee });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }

        }

        // POST api/<ResourceEmployeesController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] ResourceEmployee resourceemployee)
        //{
        //    try
        //    {
        //        await _resourceemployee.Insert(resourceemployee);
        //        return Ok($"Data Employee {resourceemployee.EmployeeName} berhasil ditambahkan");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostRE resourceemployee)
        {
            try
            {
                var npp = from re in _context.ResourceEmployees
                        where re.Npp == resourceemployee.Npp
                        select re;
                if(npp.Count() > 0)
                {
                    return BadRequest("Npp telah digunakan");
                }
                else
                {
                    ResourceEmployee inpt = new ResourceEmployee();
                    inpt.EmployeeName = resourceemployee.EmployeeName;
                    inpt.Npp = resourceemployee.Npp;
                    inpt.Email = resourceemployee.Email;
                    inpt.Phone = resourceemployee.Phone;
                    inpt.ActiveDate = resourceemployee.ActiveDate;
                    inpt.LastWorkDate = resourceemployee.LastWorkDate;
                    inpt.TotalManhour = resourceemployee.TotalManhour;
                    inpt.ResourceType = resourceemployee.ResourceType;
                    if(resourceemployee.ResourceType == 2)
                    {
                        var manmonth = await _manmonth.GetById(resourceemployee.VendorId.ToString());
                        if (manmonth.AvailableManmonth > 0)
                        {
                            inpt.VendorId = resourceemployee.VendorId;
                        }
                        else if (manmonth.AvailableManmonth <= 0)
                        {
                            return BadRequest("Kapasitas vendor melebihi batas");
                        }
                    }
                   else if (resourceemployee.ResourceType == 1)
                    {
                        inpt.VendorId = null;
                    }
                    inpt.JenjabId = resourceemployee.JenjabId;
                    inpt.DivisiId = resourceemployee.DivisiId;
                    inpt.KelompokId = resourceemployee.KelompokId;
                    inpt.Role = resourceemployee.Role;
                    inpt.ProjectExp = resourceemployee.ProjectExp;
                    inpt.Status = resourceemployee.Status;
                    inpt.CreatedBy = resourceemployee.CreatedBy;
                    inpt.CreatedTime = DateTime.Now;
                    _context.ResourceEmployees.Add(inpt);
                    _context.SaveChanges();
                    if (resourceemployee.ResourceType == 2)
                    {
                        var manmonth = await _manmonth.GetById(resourceemployee.VendorId.ToString());
                        var resourceVendor = from re in _context.ResourceEmployees
                                             where re.VendorId == resourceemployee.VendorId
                                             where re.Status == 1
                                             select re;
                        var countResourceVendor = resourceVendor.Count();
                        //var manmonth = await _manmonth.GetById(resourceemployee.VendorId.ToString());
                        manmonth.UsageManmonth = countResourceVendor;
                        manmonth.AvailableManmonth = manmonth.TotalManmonth - countResourceVendor;
                        await _context.SaveChangesAsync();
                    }

                    if (resourceemployee.ListSkill.Count > 0)
                    {
                        foreach (ListSkillEmp xx in resourceemployee.ListSkill)
                        {
                            EmployeeSkill ESkil = new EmployeeSkill();
                            ESkil.EmployeeId = inpt.EmployeeId;
                            ESkil.SkillsetId = xx.SkillId;
                            await _context.EmployeeSkills.AddAsync(ESkil);
                        }
                        _context.SaveChanges();
                    }

                    // PUT api/<ResourceEmployeesController>/5
                    return Ok($"Data Employee {resourceemployee.EmployeeName} berhasil ditambahkan");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PostRE resourceemployee)
        {
            try
            {
                await _resourceemployee.Update2(id.ToString(), resourceemployee);
                return Ok($"Data Employee ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ResourceEmployeesController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _resourceemployee.Delete(id.ToString());
        //        return Ok($"Data Employee {id} berhasil dihapus");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
