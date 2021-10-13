using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class JenjabData : IJenjab
    {
        private ProteamContext _db;
        public JenjabData(ProteamContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result !=null)
            {
                try
                {
                    _db.Jenjabs.Remove(result);
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

        public async Task<IEnumerable<Jenjab>> GetAll()
        {
            var result = await (from d in _db.Jenjabs
                               orderby d.JenjangJabatan
                               select d).ToListAsync();
            return result;
        }

        public async Task<Jenjab> GetById(string id)
        {
            var result = await (from d in _db.Jenjabs
                                where d.JenjabId == Convert.ToInt32(id)
                                select d).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Jenjab obj)
        {
            try
            {
                _db.Jenjabs.Add(obj);
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

        public async Task Update(string id, Jenjab obj)
        {
            try
            {
                var result = await GetById(id);
                if (result !=null)
                {
                    result.JenjangJabatan = obj.JenjangJabatan;
                    result.Cost = obj.Cost;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdateTime = obj.UpdateTime;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Data id = {id} tidak ditemukan");
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
