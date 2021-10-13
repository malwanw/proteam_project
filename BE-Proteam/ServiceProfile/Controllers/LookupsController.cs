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
    public class LookupsController : ControllerBase
    {
        private ILookup _lookup;
        public ProteamContext _context;

        public LookupsController(ILookup lookup, ProteamContext context)
        {
            _context = context;
            _lookup = lookup;
        }
        // GET: api/<LookupsController>
        //[HttpGet]
        //public async Task<IEnumerable<Lookup>> Get()
        //{
        //    try
        //    {
        //        List<ContohData> data = new List<ContohData>();

        //        data = StoredProcedureExecutor.ExecuteSPList<ContohData>(_context, "C_Test", new SqlParameter[]{
        //                new SqlParameter("@Flag", 1),
        //                new SqlParameter("@Parameter1", "Test"),
        //                new SqlParameter("@Parameter2", "Test")
        //        });
        //    }
        //    catch (Exception ex)
        //    { 
        //    }
        //    var results = await _lookup.GetAll();
        //    return results;
        //}


        //[HttpGet]
        //public  async Task<IActionResult>  Get(string Data)
        //{

        //    try
        //    {
        //        List<ContohData> data = new List<ContohData>();

        //        data =  StoredProcedureExecutor.ExecuteSPList<ContohData>(_context, "C_Test", new SqlParameter[]{
        //                new SqlParameter("@Flag", 1),
        //                new SqlParameter("@Parameter1", "Test"),
        //                new SqlParameter("@Parameter2", "Test")

        //        });

        //        return Ok(new { data });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error : " + ex.Message);
        //    }
        //    var results = await _lookup.GetAll();

        //}
        [HttpGet]
        public async Task<IEnumerable<Lookup>> Get()
        {
            var results = await _lookup.GetAll();
            return results;
        }

        // GET api/<LookupsController>/5
        [HttpGet("{id}")]
        public async Task<Lookup> Get(int id)
        {
            var result = await _lookup.GetById(id.ToString());
            return result;
        }
        [HttpGet("GetType/{type}")]
        public async Task<IEnumerable<Lookup>> GetType(string type)
        {
            var result = await _lookup.GetByType(type);
            return result;
        }

        [HttpGet("{type}/{statusid}")]
        public async Task<IEnumerable<Lookup>> GetTypeStatus(string type, int statusid)
        {
            var result = await _lookup.GetByTypeStatus(type, statusid.ToString());
            return result;
        }

        [HttpGet("GetTypeValue/{type}/{value}")]
        public async Task<Lookup> GetTypeValue(string type, int value)
        {
            var result = await _lookup.GetByTypeValue(type, value.ToString());
            return result;
        }

        // POST api/<LookupsController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Lookup lookup)
        //{
        //    try
        //    {
        //        await _lookup.Insert(lookup);
        //        return Ok($"Data  lookup tipe {lookup.Type} nama {lookup.Name} berhasil ditambahkan");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostLookup lookup)
        {
            try
            {
                Lookup inpt = new Lookup();
                inpt.Type = lookup.Type;
                inpt.Name = lookup.Name;
                inpt.OrderNumber = lookup.OrderNumber;
                inpt.Value = lookup.Value;
                inpt.Status = lookup.Status;
                inpt.CreatedBy = lookup.CreatedBy;
                inpt.CreatedTime = DateTime.Now;
                _context.Lookups.Add(inpt);
                _context.SaveChanges();
                return Ok($"Data lookup tipe {lookup.Type} nama {lookup.Name} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

          }

        // PUT api/<LookupsController>/5
            [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Lookup lookup)
        {
            try
            {
                await _lookup.Update(id.ToString(), lookup);
                return Ok($"Data Lookup ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<LookupsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lookup.Delete(id.ToString());
                return Ok($"Data lookup {id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
