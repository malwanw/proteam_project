using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public interface ICrud<T>
    {
        //task = method asyn
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Insert(T obj);
        Task Update(string id, T obj);
        Task Delete(string id);
    }
}
