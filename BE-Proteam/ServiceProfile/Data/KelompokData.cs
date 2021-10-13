using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class KelompokData : IKelompok
    {
        private ProteamContext _db;

        public KelompokData(ProteamContext db)
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
                    _db.Kelompoks.Remove(result);
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

        public async Task<IEnumerable<Kelompok>> GetAll()
        {
            var results = await (from k in _db.Kelompoks
                                 select k).ToListAsync();
            return results;
        }

        public async Task<Kelompok> GetById(string id)
        {
            var result = await(from k in _db.Kelompoks
                               where k.KelompokId == Convert.ToInt32(id)
                               select k).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<Kelompok>> GetByDivisiStatus(string divisi, string status)
        {
            var result = await (from k in _db.Kelompoks
                                where k.DivisiId == Convert.ToInt32(divisi) && k.Status == Convert.ToInt32(status)
                                select k).ToListAsync();

            return result;
        }

        public async Task Insert(Kelompok obj)
        {
            try
            {
                _db.Kelompoks.Add(obj);
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

        public async Task Update(string id, Kelompok obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.KelompokName = obj.KelompokName;
                    result.DivisiId = obj.DivisiId;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdateTime = DateTime.Now;
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
