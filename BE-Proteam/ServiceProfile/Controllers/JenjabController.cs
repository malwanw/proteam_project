using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceProfile.Data;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JenjabController : ControllerBase
    {
        private IJenjab _jenjab;
      

        public JenjabController(IJenjab jenjab)
        {
            _jenjab = jenjab;
        }
        // GET: api/<JenjabController>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Jenjab>> Get()
        {
            var result = await _jenjab.GetAll();
            return result;
        }

        // GET api/<JenjabController>/5
        [HttpGet("{id}")]
        public async Task<Jenjab> Get(int id)
        {
            var result = await _jenjab.GetById(id.ToString());
            return result;
        }

        // POST api/<JenjabController>
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] Jenjab jenjab)
        {
            try
            {
                await _jenjab.Insert(jenjab);
                return Ok($"Jenjab {jenjab.JenjangJabatan} sukses ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<JenjabController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Jenjab jenjab)
        {
            try
            {
                await _jenjab.Update(id.ToString(), jenjab);
                return Ok($"Data Jenjab id = {id} sukses diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<JenjabController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _jenjab.Delete(id.ToString());
                return Ok($"Data Jenjang Jabatan id = {id} sukses diupdate ");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
