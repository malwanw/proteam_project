using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public class UnitProfilingData : IUnitProfiling
    {
        private ProteamContext _db;
        public UnitProfilingData(ProteamContext db)
        {
            _db = db;
        }
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UnitProfiling>> GetAll()
        {
            var results = await (from u in _db.UnitProfilings
                                 select u).ToListAsync();
            return results;
        }

        public async Task<UnitProfiling> GetById(string id)
        {
            var result = await (from u in _db.UnitProfilings
                                where u.KelompokId == Convert.ToInt32(id)
                                select u).FirstOrDefaultAsync();

            return result;
        }

        public Task Insert(UnitProfiling obj)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, UnitProfiling obj)
        {
            throw new NotImplementedException();
        }
    }
}
