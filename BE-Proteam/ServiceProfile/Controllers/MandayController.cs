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
    public class MandayController : ControllerBase
    {
        private IManday _manday;
        public ProteamContext _context;

        public MandayController(IManday manday, ProteamContext context)
        {
            _context = context;
            _manday = manday;
            

        }
        // GET: api/<MandayController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<MandaysStatus> data = new List<MandaysStatus>();


                data = StoredProcedureExecutor.ExecuteSPList<MandaysStatus>(_context, "manday_status", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });                

                return Ok(new { data });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _manday.GetAll();
        }
            //[Route("GetAll")]
            //public async Task<IEnumerable<Manday>> Get()
            //{
            //    var result = await _manday.GetAll();
            //    return (IEnumerable<Manday>)result;
            //}

            // GET api/<MandayController>/5
            [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                List<MandaysStatus> data = new List<MandaysStatus>();


                data = StoredProcedureExecutor.ExecuteSPList<MandaysStatus>(_context, "manday_status", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                });

                return Ok(new { data });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _manday.GetAll();
        }

        //public async Task<Manday> Get(int id)
        //{
        //    var result = await _manday.GetById(id.ToString());
        //    return result;
        //}

        // POST api/<MandayController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Manday manday)
        //{
        //    try
        //    {
        //        await _manday.Insert(manday);
        //        return Ok($"Manday {manday.MandaysId} sukses ditambahkan");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostManday manday)
        {
            try
            {
                Manday inpt = new Manday();
                inpt.VendorName = manday.VendorName;
                inpt.ContractNumber = manday.ContractNumber;
                inpt.TotalMandays = manday.TotalMandays;
                inpt.UsageMandays = manday.UsageMandays;
                inpt.AvailableMandays = manday.AvailableMandays;
                inpt.StartContract = manday.StartContract;
                inpt.LastContract = manday.LastContract;
                inpt.Status = manday.Status;
                inpt.Notes = manday.Notes;
                inpt.CreatedBy = manday.CreatedBy;
                inpt.CreatedTime = DateTime.Now;
                _context.Mandays.Add(inpt);
                _context.SaveChanges();
                // PUT api/<ResourceEmployeesController>/5
                return Ok($"Manday vendor {manday.VendorName} sukses ditambahkan"); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<MandayController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Manday manday)
        {
            try
            {
                await _manday.Update(id.ToString(), manday);
                return Ok($"Manday id {id} sukses diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<MandayController>/5
        [HttpDelete("{id}")]
        [NonAction]
        public void Delete(int id)
        {
        }
    }
}