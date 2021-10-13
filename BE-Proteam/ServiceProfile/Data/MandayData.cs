using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class MandayData : IManday
    {
        private ProteamContext _db;
        public MandayData(ProteamContext db)
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
                    _db.Mandays.Remove(result);
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

        public async Task<IEnumerable<Manday>> GetAll()
        {
            var result = await (from d in _db.Mandays
                                orderby d.MandaysId
                                select d).ToListAsync();
            return result;
        }

        public async Task<Manday> GetById(string id)
        {
            var getid = await (from m in _db.Mandays
                               where m.MandaysId == Convert.ToInt32(id)
                               select m).FirstOrDefaultAsync();
            return getid;
        }

        public async Task Insert(Manday obj)
        {
            try
            {
                _db.Mandays.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Db Error:  {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }

        public async Task Update(string id, Manday obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.VendorName = obj.VendorName;
                    result.ContractNumber = obj.ContractNumber;
                    result.TotalMandays = obj.TotalMandays;
                    result.UsageMandays = obj.UsageMandays;
                    result.AvailableMandays = obj.AvailableMandays;
                    result.StartContract = obj.StartContract;
                    result.LastContract = obj.LastContract;
                    result.Notes = obj.Notes;
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