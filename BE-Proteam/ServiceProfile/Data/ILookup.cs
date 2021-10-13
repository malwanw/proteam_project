using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public interface ILookup: ICrud<Lookup>
    {
        Task<IEnumerable<Lookup>> GetByType(string type);
        Task<IEnumerable<Lookup>> GetByTypeStatus(string type, string status);
        Task<Lookup> GetByTypeValue(string type, string value);
    }
}
