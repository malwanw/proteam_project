using Microsoft.EntityFrameworkCore;
using ServiceProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ServiceProfile.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using MasterRole_GetById.Tools;

namespace ServiceProfile.Data
{
    public class ResourceEmployeeData : IResourceEmployee
    {
        private ProteamContext _db;

        public ResourceEmployeeData(ProteamContext db)
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
                    _db.ResourceEmployees.Remove(result);
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
        //#region LoadDataInboxRM
       // [Authorize]
       // [HttpPost]
        //public async Task<List<Vm>> GetDataInboxRM()
        //{

        //    List<Vm> res = new List<Vm>();
        //    try
        //    {
        //        List<Vm> list = new List<Vm>();
              

        //        list = StoredProcedureExecutor.ExecuteSPList<Vm>(_db, "sp_data", new SqlParameter[]{
        //               });

              

               
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
               
        //        return res;
        //    }
        //}
       // #endregion
        public async Task<IEnumerable<ResourceEmployee>> GetAll()
        {

             var results = await (from re in _db.ResourceEmployees.Include(e => e.Jenjab).Include(e => e.Kelompok)
                                 select re).ToListAsync();
            // var employeeskills = await (from e in _db.EmployeeSkills.Include(e => e.Skillset)
            //                            select e).AsNoTracking().ToListAsync();
            // for(int i=0; i < results.Count(); i++)
            // {
            //     results[i].EmployeeSkills = employeeskills;
            //var countsql = "exec SP_Select_Count_DynamicKendoGrid "
            //                     + "@KendoGrid='" + filter + "'";
            //int totalRow = _db.Database.SqlQuery<int>(countsql).First();

            //var xxxa = await _db.ResourceEmployees.FromSqlRaw("SELECT * FROM ResourceEmployee").ToListAsync();

            //   var xxxa = await _db.ResourceEmployees.FromSqlRaw("SELECT * FROM ResourceEmployee").ToListAsync();
            //  //var result = await context.SomeModels.FromSql("SQL_SCRIPT").ToListAsync();

            //  List<Vm> Data34 = xxxa.ToList();

            //}234

            return results;
        }

        public async Task<ResourceEmployee> GetById(string id)
        {
            var result = await(from re in _db.ResourceEmployees.Include(e => e.Jenjab).Include(e => e.Kelompok)
                               where re.EmployeeId == Convert.ToInt32(id)
                               select re).FirstOrDefaultAsync();

            return result;
        }
      

        public async Task Insert(ResourceEmployee obj)
        {
               try
            {
                _db.ResourceEmployees.Add(obj);
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

        public async Task Update(string id, ResourceEmployee obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.EmployeeName = obj.EmployeeName;
                    result.Email = obj.Email;
                    result.Phone = obj.Phone;
                    result.ActiveDate = obj.ActiveDate;
                    result.LastWorkDate = obj.LastWorkDate;
                    result.TotalManhour = obj.TotalManhour;
                    result.ResourceType = obj.ResourceType;
                    result.JenjabId = obj.JenjabId;
                    result.KelompokId = obj.KelompokId;
                    result.Role = obj.Role;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdateTime = obj.UpdateTime;
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

        public async Task<Manmonth> GetManmonthById(string id)
        {
            var getid = await (from m in _db.Manmonth
                               where m.ManmonthId == Convert.ToInt32(id)
                               select m).FirstOrDefaultAsync();
            return getid;
        }
        public async Task Update2(string id, PostRE obj)
        {

           
            try
            {
                var result = await GetById(id);
                var manmonthAfter = await GetManmonthById(obj.VendorId.ToString());
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.EmployeeName = obj.EmployeeName;
                    //result.Npp = obj.Npp;
                    result.Email = obj.Email;
                    result.Phone = obj.Phone;
                    //result.Npp = result.Npp;
                    result.ActiveDate = obj.ActiveDate;
                    result.LastWorkDate = obj.LastWorkDate;
                    result.TotalManhour = obj.TotalManhour;
                   
                    //jika type tetap os
                    if (result.ResourceType == 2 && obj.ResourceType == 2)
                    {
                        //jika vendor berubah dan kapasitas masih mencukupi
                        if (result.VendorId != obj.VendorId && manmonthAfter.AvailableManmonth > 0)
                        {
                            //jika status awal aktif
                            if (result.Status == 1)
                            {
                                var manmonthBefore = await GetManmonthById(result.VendorId.ToString());
                                //jumlah resource vendor awal dikurangi
                                manmonthBefore.UsageManmonth = manmonthBefore.UsageManmonth - 1;
                                manmonthBefore.AvailableManmonth = manmonthBefore.TotalManmonth - manmonthBefore.UsageManmonth;
                                //await _db.SaveChangesAsync();
                            }
                            //update vendor id
                            result.ResourceType = obj.ResourceType;
                            result.VendorId = obj.VendorId;
                        //jika vendor berubah dan kapasitas tidak mencukupi
                        }else if (result.VendorId != obj.VendorId && manmonthAfter.AvailableManmonth <= 0)
                        {
                            throw new Exception("Kapasitas vendor melebihi batas");
                        }
                        //jika vendor tidak berubah
                        else
                        {
                            result.ResourceType = obj.ResourceType;
                            result.VendorId = obj.VendorId;
                        }
                    }
                    //jika diubah dari os diubah ke fte
                    else if (result.ResourceType == 2 && obj.ResourceType == 1)
                    {
                        //jika status awal aktif
                        if (result.Status == 1)
                        {
                            var manmonthBefore = await GetManmonthById(result.VendorId.ToString());
                            //jumlah resource vendor awal dikurangi
                            manmonthBefore.UsageManmonth = manmonthBefore.UsageManmonth - 1;
                            manmonthBefore.AvailableManmonth = manmonthBefore.TotalManmonth - manmonthBefore.UsageManmonth;
                        }
                        //vendor menjadi null
                        result.ResourceType = obj.ResourceType;
                        result.VendorId = null;
                    }
                    //jika diubah dari fte ke os
                    else if(result.ResourceType == 1 && obj.ResourceType == 2 )
                    {
                        //jika kapasitas vendor masih mencukupi
                        if(manmonthAfter.AvailableManmonth > 0)
                        {
                            result.ResourceType = obj.ResourceType;
                            result.VendorId = obj.VendorId;
                        //jika kapasitas vendor tidak mencukupi
                        }else if(manmonthAfter.AvailableManmonth <= 0)
                        {
                            throw new Exception("Kapasitas vendor telah penuh");
                        }
                       
                    }
                    else
                    {
                        result.ResourceType = obj.ResourceType;
                        result.VendorId = null;
                    }
                    result.JenjabId = obj.JenjabId;
                    result.DivisiId = obj.DivisiId;
                    result.KelompokId = obj.KelompokId;
                    result.ProjectExp = obj.ProjectExp;
                    result.Role = obj.Role;
                    result.Status = obj.Status;
                    result.UpdatedBy = obj.UpdatedBy;
                    result.UpdateTime = DateTime.Now;
                    await _db.SaveChangesAsync();
                    if (obj.ResourceType == 2)
                    {
                        var resourceVendor = from re in _db.ResourceEmployees
                                             where re.VendorId == obj.VendorId
                                             where re.Status == 1
                                             select re;
                        var countResourceVendor = resourceVendor.Count();
                        //var manmonth = await GetManmonthById(obj.VendorId.ToString());
                        manmonthAfter.UsageManmonth = countResourceVendor;
                        manmonthAfter.AvailableManmonth = manmonthAfter.TotalManmonth - countResourceVendor;
                        await _db.SaveChangesAsync();
                    }
                    var employeeSkill = from es in _db.EmployeeSkills
                                        where es.EmployeeId == Convert.ToInt32(id)
                                        select es;
                    foreach (var es in employeeSkill)
                    {
                        _db.EmployeeSkills.Remove(es);
                    }
                    _db.SaveChanges();
                    if (obj.ListSkill.Count > 0)
                    {

                        foreach (ListSkillEmp xx in obj.ListSkill)
                        {
                            EmployeeSkill ESkil = new EmployeeSkill();
                            ESkil.EmployeeId = result.EmployeeId;
                            ESkil.SkillsetId = xx.SkillId;
                            await _db.EmployeeSkills.AddAsync(ESkil);
                        }
                        _db.SaveChanges();
                    }
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
