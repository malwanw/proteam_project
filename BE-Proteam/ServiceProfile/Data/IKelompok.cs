using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public interface IKelompok: ICrud<Kelompok>
    {
        Task<IEnumerable<Kelompok>> GetByDivisiStatus(string divisi, string status);
    }
}
