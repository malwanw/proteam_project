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
    public class ManmonthController : ControllerBase
    {
        private IManmonth _manmonth;
        public ProteamContext _context;

        public ManmonthController(IManmonth manmonth, ProteamContext context)
        {
            _context = context;
            _manmonth = manmonth;
            

        }
        // GET: api/<MandayController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<GetManmonth> data = new List<GetManmonth>();


                data = StoredProcedureExecutor.ExecuteSPList<GetManmonth>(_context, "sp_manmonth", new SqlParameter[]{
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
            var results = await _manmonth.GetAll();
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
                List<GetManmonth> data = new List<GetManmonth>();


                data = StoredProcedureExecutor.ExecuteSPList<GetManmonth>(_context, "sp_manmonth", new SqlParameter[]{
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
            var results = await _manmonth.GetAll();
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
        public async Task<IActionResult> Post([FromBody] PostManmonth manmonth)
        {
            try
            {
                Manmonth inpt = new Manmonth();
                inpt.VendorName = manmonth.VendorName;
                inpt.ContractNumber = manmonth.ContractNumber;
                inpt.TotalManmonth = manmonth.TotalManmonth;
                inpt.UsageManmonth = 0;
                inpt.AvailableManmonth = manmonth.TotalManmonth;
                inpt.StartContract = manmonth.StartContract;
                inpt.LastContract = manmonth.LastContract;
                inpt.Status = manmonth.Status;
                inpt.Notes = manmonth.Notes;
                inpt.CreatedBy = manmonth.CreatedBy;
                inpt.CreatedTime = DateTime.Now;
                _context.Manmonth.Add(inpt);
                _context.SaveChanges();
                // PUT api/<ResourceEmployeesController>/5
                return Ok($"Manday vendor {manmonth.VendorName} sukses ditambahkan"); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        // PUT api/<MandayController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Manmonth manmonth)
        {
            try
            {
                await _manmonth.Update(id.ToString(), manmonth);
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