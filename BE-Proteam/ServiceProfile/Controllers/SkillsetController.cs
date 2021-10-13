using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ServiceProfile.Data;
using ServiceProfile.Models;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsetController : ControllerBase
    {
        private ISkillset _skillset;

        public SkillsetController (ISkillset skillset)
        {
            _skillset = skillset;
        }
        // GET: api/<SkillsetController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Skillset>> Get()
        {
            var result = await _skillset.GetAll();
            return result;
        }

        // GET api/<SkillsetController>/5
        [HttpGet("{id}")]
        public async Task<Skillset> Get(int id)
        {
            var result = await _skillset.GetById(id.ToString());
            return result;
        }

        // POST api/<SkillsetController>
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] Skillset skillset)
        {
            try
            {
                await _skillset.Insert(skillset);
                return Ok($"Skillset {skillset.Skillset1} sukses ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SkillsetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Skillset skillset)
        {
            try
            {
                await _skillset.Update(id.ToString(), skillset);
                return Ok($"Skillset {id} sukses diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SkillsetController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _skillset.Delete(id.ToString());
        //        return Ok($"Skillset {id} sukses dihapus");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
