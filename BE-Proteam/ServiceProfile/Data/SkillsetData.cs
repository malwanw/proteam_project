using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;

namespace ServiceProfile.Data
{
    public class SkillsetData : ISkillset
    {
        private ProteamContext _db;
        public SkillsetData(ProteamContext db)
        {
            _db = db;
        }

        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result != null)
            {
                try
                {
                    _db.Skillsets.Remove(result);
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

        public async Task<IEnumerable<Skillset>> GetAll()
        {
            var result = await (from d in _db.Skillsets
                                orderby d.Skillset1
                                select d).ToListAsync();
            return result;
        }

        public async Task<Skillset> GetById(string id)
        {
            var result = await (from d in _db.Skillsets
                                where d.SkillsetId == Convert.ToInt32(id)
                                select d).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Skillset obj)
        {
            try
            {
                _db.Skillsets.Add(obj);
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

        public async Task Update(string id, Skillset obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.Skillset1 = obj.Skillset1;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdateTime = obj.UpdateTime;
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
