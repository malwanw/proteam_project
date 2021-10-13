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
    public class EmployeeSkillsController : ControllerBase
    {
        private IEmployeeSkill _employeeskill;
        public EmployeeSkillsController(IEmployeeSkill employeeskill)
        {
            _employeeskill = employeeskill;
        }
        // GET: api/<EmployeeSkillsController>
        [HttpGet]
        public async Task<IEnumerable<EmployeeSkill>> Get()
        {
            var results = await _employeeskill.GetAll();
            return results;
        }

        

        // GET api/<EmployeeSkillsController>/5
        [HttpGet("{id}")]
        public async Task<EmployeeSkill> Get(int id)
        {
            var result = await _employeeskill.GetById(id.ToString());
            return result;
        }

        // POST api/<EmployeeSkillsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeSkill employeeskill)
        {
            try
            {
                await _employeeskill.Insert(employeeskill);
                return Ok($"Data Empliyee Skill berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EmployeeSkillsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeSkill employeeskill)
        {
            try
            {
                await _employeeskill.Update(id.ToString(), employeeskill);
                return Ok($"Data Employee Skill ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EmployeeSkillsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeskill.Delete(id.ToString());
                return Ok($"Data Employee Skill Id {id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
