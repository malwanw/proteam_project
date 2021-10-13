using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class EmployeeSkillData : IEmployeeSkill
    {
        private ProteamContext _db;

        public EmployeeSkillData(ProteamContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            var result = await GetById(id);
            //cek apakah data ada?
            if (result != null)
            {
                try
                {
                    _db.EmployeeSkills.Remove(result);
                    await _db.SaveChangesAsync();
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

        public async Task<IEnumerable<EmployeeSkill>> GetAll()
        {
            var results = await (from es in _db.EmployeeSkills
                                 select es).ToListAsync();
            return results;
        }

        public async Task<EmployeeSkill> GetById(string id)
        {
            var result = await (from es in _db.EmployeeSkills
                                where es.EmpSkillId == Convert.ToInt32(id)
                                select es).FirstOrDefaultAsync();

            return result;
        }

        public async Task Insert(EmployeeSkill obj)
        {
            try
            {
                _db.EmployeeSkills.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Db Error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task Update(string id, EmployeeSkill obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.EmployeeId = obj.EmployeeId;
                    result.SkillsetId = obj.SkillsetId;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Data id:{id} tidak ditemukan");
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
