

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
    public class UsersController : ControllerBase
    {
        private IUser _user;
        public ProteamContext _context;
        public UsersController(IUser user, ProteamContext context)
        {
            _context = context;
            _user = user;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] PostUser user)
        {
            try
            {
                var npp = from re in _context.DataUsers
                          where re.Npp == user.Npp
                          select re;
                if (npp.Count() > 0)
                {
                    return BadRequest("NPP telah terdaftar");
                } 
                else
                {
                    await _user.Registration(user);
                    DataUser inpt = new DataUser();
                    inpt.FullName = user.FullName;
                    inpt.Email = user.Email;
                    inpt.Phone = user.Phone;
                    inpt.Npp = user.Npp;
                    inpt.KelompokId = user.KelompokId;
                    inpt.Role = user.Role;
                    inpt.Status = user.Status;
                    inpt.CreatedBy = user.CreatedBy;
                    inpt.CreatedTime = DateTime.Now;
                    _context.DataUsers.Add(inpt);
                    _context.SaveChanges();
                    return Ok("Proses Registrasi Berhasil");
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Authentication")]
        public async Task<IActionResult> Authentication([FromBody] PostUser userParam)
        {
           
            var user = await _user.Authenticate(userParam.Npp, userParam.Password);
            if (user == null)
            {
                return BadRequest("NPP/Password anda salah");
            }else if(user.Status == 0)
            {
                return BadRequest($"Akun dengan NPP {user.Npp} tidak aktif");
            }
            return Ok(user);
        }

       
        // GET: api/<UsersController>
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<PostUser> userdata = new List<PostUser>();
                List<PostUser> newuserdata = new List<PostUser>();


                userdata = StoredProcedureExecutor.ExecuteSPList<PostUser>(_context, "sp_user_data", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (PostUser ud in userdata)
                {
                    PostUser newData = new PostUser();
                    newData.Id = ud.Id;
                    newData.FullName = ud.FullName;
                    newData.Npp = ud.Npp;
                    newData.Email = ud.Email;
                    newData.Phone = ud.Phone;
                    newData.Role = ud.Role;
                    newData.RoleName = ud.RoleName;
                    newData.KelompokId = ud.KelompokId;
                    newData.Kelompok = ud.Kelompok;
                    newData.Status = ud.Status;
                    newData.StatusName = ud.StatusName;
                    newData.CreatedBy = ud.CreatedBy;
                    newData.UpdatedBy = ud.UpdatedBy;
                    newData.CreatedTime = ud.CreatedTime;
                    newData.UpdateTime = ud.UpdateTime;
                    newuserdata.Add(newData);
                }

                return Ok(new { newuserdata });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _user.GetAll();

        }

        [HttpGet("GetByNpp/{npp}")]
        public async Task<IActionResult> GetByNpp(string npp)
        {
            try
            {
                List<PostUser> userdata = new List<PostUser>();
                List<PostUser> newuserdata = new List<PostUser>();


                userdata = StoredProcedureExecutor.ExecuteSPList<PostUser>(_context, "sp_user_data", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", npp),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (PostUser ud in userdata)
                {
                    PostUser newData = new PostUser();
                    newData.Id = ud.Id;
                    newData.FullName = ud.FullName;
                    newData.Npp = ud.Npp;
                    newData.Email = ud.Email;
                    newData.Phone = ud.Phone;
                    newData.Role = ud.Role;
                    newData.RoleName = ud.RoleName;
                    newData.KelompokId = ud.KelompokId;
                    newData.Kelompok = ud.Kelompok;
                    newData.Status = ud.Status;
                    newData.StatusName = ud.StatusName;
                    newData.CreatedBy = ud.CreatedBy;
                    newData.UpdatedBy = ud.UpdatedBy;
                    newData.CreatedTime = ud.CreatedTime;
                    newData.UpdateTime = ud.UpdateTime;
                    newuserdata.Add(newData);
                }

                return Ok(new { newuserdata });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _user.GetAll();

        }

        // GET api/<UsersController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                List<PostUser> userdata = new List<PostUser>();
                List<PostUser> newuserdata = new List<PostUser>();


                userdata = StoredProcedureExecutor.ExecuteSPList<PostUser>(_context, "sp_user_data", new SqlParameter[]{
                        new SqlParameter("@Flag", 3),
                        new SqlParameter("@Parameter1", id),
                        new SqlParameter("@Parameter2", "Test")
                });


                foreach (PostUser ud in userdata)
                {
                    PostUser newData = new PostUser();
                    newData.Id = ud.Id;
                    newData.FullName = ud.FullName;
                    newData.Npp = ud.Npp;
                    newData.Email = ud.Email;
                    newData.Phone = ud.Phone;
                    newData.Role = ud.Role;
                    newData.RoleName = ud.RoleName;
                    newData.KelompokId = ud.KelompokId;
                    newData.Kelompok = ud.Kelompok;
                    newData.Status = ud.Status;
                    newData.StatusName = ud.StatusName;
                    newData.CreatedBy = ud.CreatedBy;
                    newData.UpdatedBy = ud.UpdatedBy;
                    newData.CreatedTime = ud.CreatedTime;
                    newData.UpdateTime = ud.UpdateTime;
                    newuserdata.Add(newData);
                }

                return Ok(new { newuserdata });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _user.GetAll();

        }
        // POST api/<UsersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<UsersController>/5
        [HttpPut("updatebyid/{id}")]
        public async Task<IActionResult> PutById(int id, [FromBody] DataUser user)
        {
            try
            {
                await _user.Update(id.ToString(), user);
                return Ok($"Data user Id: {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updatebynpp/{npp}")]
        public async Task<IActionResult> PutByNpp(string npp, [FromBody] DataUser user)
        {
            try
            {
                await _user.Update2(npp, user);
                return Ok($"Data user NPP: {npp} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updatepassword/{npp}")]
        public async Task<IActionResult> PutPassword(string npp, [FromBody] UpdatePassword up)
        {
            try
            {
                    await _user.UpdatePasswordByNpp(npp, up);
                    return Ok("Password berhasil diubah");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
