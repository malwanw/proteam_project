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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitProfileController : ControllerBase
    {
        private IUnitProfiling _unitprofile;
        public ProteamContext _context;

        public UnitProfileController(IUnitProfiling unitprofile, ProteamContext context)
        {
            _context = context;
            _unitprofile = unitprofile;
        }

        // GET: api/<UnitProfileController>
        [HttpGet]
        public async Task<IEnumerable<UnitProfiling>> Get()
        {
            var results = await _unitprofile.GetAll();
            return results;
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail()
        {

            try
            {
                List<UnitProfile> unit = new List<UnitProfile>();
                List<UnitProfile> unitdata = new List<UnitProfile>();
                List<Member> member = new List<Member>();

                unit = StoredProcedureExecutor.ExecuteSPList<UnitProfile>(_context, "unit_profile", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });

                foreach (UnitProfile u in unit)
                {
                    UnitProfile DataUnit = new UnitProfile();
                    DataUnit.Unit_id = u.Unit_id;
                    DataUnit.Kelompok_id = u.Kelompok_id;
                    DataUnit.KelompokName = u.KelompokName;
                    DataUnit.Divisi_id = u.Divisi_id;
                    DataUnit.DivisiName = u.DivisiName;
                    DataUnit.TotalEmployee = u.TotalEmployee;
                    DataUnit.TotalManhour = u.TotalManhour;
                    DataUnit.Skill = u.Skill;
                    DataUnit.Status = u.Status;
                    DataUnit.StatusName = u.StatusName;
                    DataUnit.CreatedTime = u.CreatedTime;
                    DataUnit.UpdatedTime = u.UpdatedTime;
                    member = StoredProcedureExecutor.ExecuteSPList<Member>(_context, "unit_profile", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", u.Kelompok_id),
                        new SqlParameter("@Parameter2", "Test")
                     });
                    DataUnit.ListMember = member;

                    unitdata.Add(DataUnit);
                }
                return Ok(new { unitdata });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _unitprofile.GetAll();

        }

        // GET api/<UnitProfileController>/5
        [HttpGet("GetByKelompokId/{id}")]
        public async Task<UnitProfiling> Get(int id)
        {
            var result = await _unitprofile.GetById(id.ToString());
            return result;
        }

        //// POST api/<UnitProfileController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UnitProfileController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UnitProfileController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
