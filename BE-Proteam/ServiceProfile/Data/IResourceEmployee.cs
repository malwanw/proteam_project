using ServiceProfile.Models;
using ServiceProfile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public interface IResourceEmployee: ICrud<ResourceEmployee>
    {
        Task Update2(string id, PostRE obj);
    }
}
