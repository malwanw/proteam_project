using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class ManmonthData : IManmonth
    {
        private ProteamContext _db;
        public ManmonthData(ProteamContext db)
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
                    _db.Manmonth.Remove(result);
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

        public async Task<IEnumerable<Manmonth>> GetAll()
        {
            var result = await (from d in _db.Manmonth
                                orderby d.ManmonthId
                                select d).ToListAsync();
            return result;
        }

        public async Task<Manmonth> GetById(string id)
        {
            var getid = await (from m in _db.Manmonth
                               where m.ManmonthId == Convert.ToInt32(id)
                               select m).FirstOrDefaultAsync();
            return getid;
        }

        public async Task Insert(Manmonth obj)
        {
            try
            {
                _db.Manmonth.Add(obj);
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

        public async Task Update(string id, Manmonth obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.VendorName = obj.VendorName;
                    result.ContractNumber = obj.ContractNumber;
                    result.TotalManmonth = obj.TotalManmonth;
                    //result.UsageManmonth = obj.UsageManmonth;
                    result.AvailableManmonth = result.TotalManmonth - result.UsageManmonth;
                    result.StartContract = obj.StartContract;
                    result.LastContract = obj.LastContract;
                    result.Notes = obj.Notes;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdatedTime = DateTime.Now;
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