using ServiceProfile.Models;
using ServiceProfile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProfile.Data
{
    public interface IUser:ICrud<DataUser>
    {
        Task<PostUser> Authenticate(string username, string password);
        Task Registration(PostUser user);

        Task Update2(string npp, DataUser obj);

        Task UpdatePasswordByNpp(string npp, UpdatePassword obj);
    }
}
