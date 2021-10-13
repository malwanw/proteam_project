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
    public class KelompoksController : ControllerBase
    {
       
        private IKelompok _kelompok;
        public ProteamContext _context;

        public KelompoksController(IKelompok kelompok, ProteamContext context)
        {
            _context = context;
            _kelompok = kelompok;
        }
        // GET: api/<KelompoksController>
        //[HttpGet]
        //public async Task<IEnumerable<Kelompok>> Get()
        //{
        //    var results = await _kelompok.GetAll();
        //    return results;
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                List<GetKelompok> kelompok = new List<GetKelompok>();
                List<GetKelompok> newKelompok = new List<GetKelompok>();
                List<SkillName> skillname = new List<SkillName>();


                kelompok = StoredProcedureExecutor.ExecuteSPList<GetKelompok>(_context, "sp_kelompok", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (GetKelompok k  in kelompok)
                {
                    GetKelompok newData = new GetKelompok();
                    newData.KelompokId = k.KelompokId;
                    newData.KelompokName = k.KelompokName;
                    newData.DivisiId = k.DivisiId;
                    newData.DivisiName = k.DivisiName;
                    newData.Status = k.Status;
                    newData.nama_status = k.nama_status;
                    newData.CreatedBy = k.CreatedBy;
                    newData.UpdatedBy = k.UpdatedBy;
                    newData.CreatedTime = k.CreatedTime;
                    newData.UpdateTime = k.UpdateTime;
                    newKelompok.Add(newData);

                }


                return Ok(new { newKelompok });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _kelompok.GetAll();

        }
        [HttpGet("GetByDivisiStatus/{divisiid}/{statusid}")]
        public async Task<IActionResult> GetyDivisiStatus(int divisiid, int statusid)
        {

            try
            {
                List<GetKelompok> kelompok = new List<GetKelompok>();
                List<GetKelompok> newKelompok = new List<GetKelompok>();
                List<SkillName> skillname = new List<SkillName>();


                kelompok = StoredProcedureExecutor.ExecuteSPList<GetKelompok>(_context, "sp_kelompok_bydivisi", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", divisiid.ToString()),
                        new SqlParameter("@Parameter2", statusid.ToString())
                });

                if (kelompok.Count() > 0)
                {
                    foreach (GetKelompok k in kelompok)
                    {
                        GetKelompok newData = new GetKelompok();
                        newData.KelompokId = k.KelompokId;
                        newData.KelompokName = k.KelompokName;
                        newData.DivisiId = k.DivisiId;
                        newData.DivisiName = k.DivisiName;
                        newData.Status = k.Status;
                        newData.nama_status = k.nama_status;
                        newData.CreatedBy = k.CreatedBy;
                        newData.UpdatedBy = k.UpdatedBy;
                        newData.CreatedTime = k.CreatedTime;
                        newData.UpdateTime = k.UpdateTime;
                        newKelompok.Add(newData);

                    }


                    return Ok(new { newKelompok });
                }
                else
                {
                    return BadRequest("Data tidak ditemukan");
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _kelompok.GetAll();

        }
        //public async Task<IEnumerable<Kelompok>> GetDivisiStatus(string divisi, string status)
        //{
        //    var result = await _kelompok.GetByDivisiStatus(divisi, status);
        //    return result;
        //}
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{

        //    try
        //    {
        //        List<KelompokProfile> kelompok = new List<KelompokProfile>();
        //        List<KelompokProfile> kelompokdata = new List<KelompokProfile>();
        //        List<Member> member = new List<Member>();
        //        List<SkillName> skillname = new List<SkillName>();


        //        kelompok = StoredProcedureExecutor.ExecuteSPList<KelompokProfile>(_context, "join_kelompok", new SqlParameter[]{
        //                new SqlParameter("@Flag", 1),
        //                new SqlParameter("@Parameter1", "Test"),
        //                new SqlParameter("@Parameter2", "Test")
        //        });

        //        foreach(KelompokProfile k in kelompok)
        //        {
        //            KelompokProfile DataKelompok = new KelompokProfile();
        //            DataKelompok.KelompokId = k.KelompokId;
        //            DataKelompok.KelompokName = k.KelompokName;
        //            DataKelompok.TotalEmployee = k.TotalEmployee;
        //            DataKelompok.TotalManhour = k.TotalManhour;
        //            DataKelompok.Status = k.Status;
        //            DataKelompok.nama_status = k.nama_status;

        //            skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "join_kelompok", new SqlParameter[]{
        //                new SqlParameter("@Flag", 2),
        //                new SqlParameter("@Parameter1", DataKelompok.KelompokId),
        //                new SqlParameter("@Parameter2", "Test")
        //            });
        //            if (skillname.Count() > 0)
        //            {
        //                string DataSkill = "";
        //                foreach (SkillName s in skillname)
        //                {
        //                    DataSkill = DataSkill + " " + s.Skillset + "</b> <br/>";
        //                }
        //                DataKelompok.Skillset = DataSkill;
        //            }

        //            member = StoredProcedureExecutor.ExecuteSPList<Member>(_context, "join_kelompok", new SqlParameter[]{
        //                new SqlParameter("@Flag", 3),
        //                new SqlParameter("@Parameter1", DataKelompok.KelompokId),
        //                new SqlParameter("@Parameter2", "Test")
        //             });
        //             DataKelompok.ListMember = member;

        //            kelompokdata.Add(DataKelompok);
        //        }
        //        return Ok(new { kelompokdata });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error : " + ex.Message);
        //    }
        //    var results = await _kelompok.GetAll();

        //}
        // GET api/<KelompoksController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{

        //    try
        //    {
        //        List<KelompokProfile> kelompok = new List<KelompokProfile>();
        //        List<KelompokProfile> kelompokdata = new List<KelompokProfile>();
        //        List<Member> member = new List<Member>();
        //        List<SkillName> skillname = new List<SkillName>();


        //        kelompok = StoredProcedureExecutor.ExecuteSPList<KelompokProfile>(_context, "join_kelompok_byid", new SqlParameter[]{
        //                new SqlParameter("@Flag", 1),
        //                new SqlParameter("@Parameter1", id),
        //                new SqlParameter("@Parameter2", "Test")
        //        });

        //        foreach (KelompokProfile k in kelompok)
        //        {
        //            KelompokProfile DataKelompok = new KelompokProfile();
        //            DataKelompok.KelompokId = k.KelompokId;
        //            DataKelompok.KelompokName = k.KelompokName;
        //            DataKelompok.TotalEmployee = k.TotalEmployee;
        //            DataKelompok.TotalManhour = k.TotalManhour;
        //            DataKelompok.Status = k.Status;
        //            DataKelompok.nama_status = k.nama_status;

        //            skillname = StoredProcedureExecutor.ExecuteSPList<SkillName>(_context, "join_kelompok_byid", new SqlParameter[]{
        //                new SqlParameter("@Flag", 2),
        //                new SqlParameter("@Parameter1", DataKelompok.KelompokId),
        //                new SqlParameter("@Parameter2", "Test")
        //            });
        //            if (skillname.Count() > 0)
        //            {
        //                string DataSkill = "";
        //                foreach (SkillName s in skillname)
        //                {
        //                    DataSkill = DataSkill + " " + s.Skillset + "</b> <br/>";
        //                }
        //                DataKelompok.Skillset = DataSkill;
        //            }
        //            member = StoredProcedureExecutor.ExecuteSPList<Member>(_context, "join_kelompok_byid", new SqlParameter[]{
        //                new SqlParameter("@Flag", 3),
        //                new SqlParameter("@Parameter1", DataKelompok.KelompokId),
        //                new SqlParameter("@Parameter2", "Test")
        //             });
        //            DataKelompok.ListMember = member;
        //            kelompokdata.Add(DataKelompok);
        //        }
        //        return Ok(new { kelompokdata });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error : " + ex.Message);
        //    }
        //    var results = await _kelompok.GetAll();

        //}

        // POST api/<KelompoksController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Kelompok kelompok)
        //{
        //    try
        //    {
        //        await _kelompok.Insert(kelompok);
        //        return Ok($"Data Kelompok {kelompok.KelompokName} berhasil ditambahkan");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostKelompok kelompok)
        {
            try
            {
                Kelompok inpt = new Kelompok();
                inpt.KelompokId = kelompok.KelompokId;
                inpt.KelompokName = kelompok.KelompokName;
                inpt.DivisiId = kelompok.DivisiId;
                inpt.Status = kelompok.Status;
                inpt.CreatedBy = kelompok.CreatedBy;
                inpt.CreatedTime = DateTime.Now;

                _context.Kelompoks.Add(inpt);
                _context.SaveChanges();
                return Ok($"Data kelompok id {kelompok.KelompokId} nama {kelompok.KelompokName} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<KelompoksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Kelompok kelompok)
        {
            try
            {
                await _kelompok.Update(id.ToString(), kelompok);
                return Ok($"Data Kelompok ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<KelompoksController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _kelompok.Delete(id.ToString());
        //        return Ok($"Data kelompok {id} berhasil dihapus");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
