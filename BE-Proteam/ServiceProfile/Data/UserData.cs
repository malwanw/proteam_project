using Microsoft.Data.SqlClient;
using ServiceProfile.Component;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceProfile.Helpers;
using ServiceProfile.Models;
using ServiceProfile.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ServiceProfile.Data
{
    public class UserData : IUser
    {
        private AppSettings _appSettings;
        private UserManager<IdentityUser> _userManager;
        private ProteamContext _db;

        //inject asp identity untuk memanggil ibjek asp identity
        public UserData(IOptions<AppSettings> appSettings,
            UserManager<IdentityUser> userManager,
            ProteamContext db)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _db = db;
        }
        public async Task<PostUser> Authenticate(string username, string password)
        {
            List<PostUser> userdata = new List<PostUser>();
            //<PostUser> newuser = new List<PostUser>();
            var user = new PostUser();
            userdata = StoredProcedureExecutor.ExecuteSPList<PostUser>(_db, "sp_user_data", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", username),
                        new SqlParameter("@Parameter2", "Test")
                });
            //cek password
            var userFind = await _userManager.CheckPasswordAsync(await _userManager.FindByNameAsync(username), password);
            if (!userFind)
                return null;

            foreach (PostUser ud in userdata)
            {
                user.FullName = ud.FullName;
                user.Npp = ud.Npp;
                user.Email = ud.Email;
                user.Phone = ud.Phone;

                //membuat jwt token
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Npp));

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = string.Empty;
                user.Role = ud.Role;
                user.RoleName = ud.RoleName;
                user.KelompokId = ud.KelompokId;
                user.Kelompok = ud.Kelompok;
                user.Status = ud.Status;
                user.CreatedBy = ud.CreatedBy;
                user.UpdatedBy = ud.UpdatedBy;
                user.CreatedTime = ud.CreatedTime;
                user.StatusName = ud.StatusName;
            }
            return user;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<DataUser> GetById(string id)
        {
            var result = await (from du in _db.DataUsers
                                where du.Id == Convert.ToInt32(id)
                                select du).FirstOrDefaultAsync();

            return result;
        }

        public async Task<DataUser> GetByNpp(string npp)
        {
            var result = await (from du in _db.DataUsers
                                where du.Npp == npp
                                select du).FirstOrDefaultAsync();

            return result;
        }

        public Task Insert(DataUser obj)
        {
            throw new NotImplementedException();
        }

        public async Task Registration(PostUser user)
        {
            //registration untuk nambah user baru
            try
            {
                var _user = new IdentityUser { UserName = user.Npp, Email = user.Email, PhoneNumber = user.Phone };
                var result = await _userManager.CreateAsync(_user, user.Password);

                if (!result.Succeeded)
                    throw new Exception("Gagal menambahkan pengguna");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task Update(string id, DataUser obj)
        {
            try
            {

                var result = await GetById(id);

                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.FullName = obj.FullName;
                    //result.Npp = obj.Npp;
                    result.Email = obj.Email;
                    result.Phone = obj.Phone;
                    result.Role = obj.Role;
                    result.KelompokId = obj.KelompokId;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdatedTime = DateTime.Now;
                    await _db.SaveChangesAsync();
                    var identityuser = await _userManager.FindByNameAsync(result.Npp);
                    identityuser.Email = obj.Email;
                    identityuser.PhoneNumber = obj.Phone;
                    await _userManager.UpdateAsync(identityuser);
                }
                else
                {
                    throw new Exception($"Data user Id:{obj.Id} tidak ditemukan");
                }
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"DbError: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        public async Task Update2(string npp, DataUser obj)
        {
            try
            {
                var result = await GetByNpp(npp);

                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.FullName = obj.FullName;
                    //result.Npp = obj.Npp;
                    result.Email = obj.Email;
                    result.Phone = obj.Phone;
                    result.Role = obj.Role;
                    result.KelompokId = obj.KelompokId;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdatedTime = DateTime.Now;
                    await _db.SaveChangesAsync();
                    var identityuser = await _userManager.FindByNameAsync(npp);
                    identityuser.Email = obj.Email;
                    identityuser.PhoneNumber = obj.Phone;
                    await _userManager.UpdateAsync(identityuser);
                }
                else
                {
                    throw new Exception($"Data user NPP:{result.Npp} tidak ditemukan");
                }
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"DbError: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        public async Task UpdatePasswordByNpp(string npp, UpdatePassword up)
        {
            try
            {
             
                var identityuser = await _userManager.FindByNameAsync(npp);
                var userFind = await _userManager.CheckPasswordAsync(await _userManager.FindByNameAsync(npp), up.CurrentPassword);
                if (!userFind)
                {
                    throw new Exception("Password anda salah");
                }
                else
                {
                   var result = await _userManager.ChangePasswordAsync(identityuser, up.CurrentPassword, up.NewPassword);
                    if (!result.Succeeded)
                        throw new Exception("Password min mengandung 8 karakter terdiri dari huruf besar, huruf kecil, angka, dan simbol");
                }
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"DbError: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}