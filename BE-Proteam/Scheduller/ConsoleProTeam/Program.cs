using ConsoleProTeam.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.cek data di table xx, truncate dulu
            ProteamEntities db = new ProteamEntities();
            string sql = " truncate table UnitProfiling " ;

            db.Database.ExecuteSqlCommand(sql);

            //get data dari sp xx
            #region updateunitprofiling
            string sql2 = " EXEC join_kelompok "
                                 + "@Flag = 1,"
                                 + "@Parameter1 = ''";
            List<ResourceVModel> DataEmp = db.Database.SqlQuery<ResourceVModel>(sql2).ToList();

            if (DataEmp.Count > 0)
            {
                foreach (ResourceVModel xx in DataEmp)
                {
                    UnitProfiling dtbaru = new UnitProfiling();
                    dtbaru.Kelompok_id = xx.KelompokId;
                    dtbaru.KelompokName = xx.KelompokName;
                    dtbaru.Divisi_id = xx.DivisiId;
                    dtbaru.DivisiName = xx.DivisiName;
                    dtbaru.TotalEmployee = xx.TotalEmployee;
                    dtbaru.TotalManhour = xx.TotalManhour;
                    dtbaru.Status = xx.Status;
                    dtbaru.StatusName = xx.nama_status;
                    dtbaru.UpdatedTime = DateTime.Now;
                    sql2 = " EXEC join_kelompok "
                                + "@Flag = 2,"
                                + "@Parameter1 = '"+xx.KelompokId+"'";
                    List<string> Skill = db.Database.SqlQuery<string>(sql2).ToList();
                    if (Skill.Count > 0)
                    {
                        foreach(string xxx in Skill)
                        {
                            if (dtbaru.Skill == null)
                            {
                                dtbaru.Skill = xxx;
                            }
                            else
                            {
                                dtbaru.Skill = dtbaru.Skill + " <br/> " + xxx;
                            }
                        }
                      
                        
                    }
                    db.UnitProfilings.Add(dtbaru);
                    db.SaveChanges();
                }
            }
            #endregion



        }
    }
}
