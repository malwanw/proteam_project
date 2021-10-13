using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class LookupData : ILookup
    {
        private ProteamContext _db;

        public LookupData(ProteamContext db)
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
                    _db.Lookups.Remove(result);
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

        public async Task<IEnumerable<Lookup>> GetAll()
        {
            var results = await (from l in _db.Lookups
                                 select l).ToListAsync();
            return results;
        }

        public async Task<Lookup> GetById(string id)
        {
            var result = await(from l in _db.Lookups
                               where l.LookupId == Convert.ToInt32(id)
                               select l).FirstOrDefaultAsync();

            return result;
        }
        public async Task<IEnumerable<Lookup>> GetByType(string type)
        {
            var result = await(from l in _db.Lookups
                               where l.Type == type
                               select l).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Lookup>> GetByTypeStatus(string type, string status)
        {
            var result = await (from l in _db.Lookups
                                where l.Type == type && l.Status == Convert.ToInt32(status)
                                select l).ToListAsync();

            return result;
        }
        public async Task<Lookup> GetByTypeValue(string type, string value)
        {
            var result = await (from l in _db.Lookups
                                where l.Type == type && l.Value == Convert.ToInt32(value)
                                select l).FirstOrDefaultAsync();

            return result;
        }
        public async Task Insert(Lookup obj)
        {
            try
            {
                _db.Lookups.Add(obj);
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

        public async Task Update(string id, Lookup obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.Type = obj.Type;
                    result.Name = obj.Name;
                    result.OrderNumber = obj.OrderNumber;
                    result.Value = obj.Value;
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
