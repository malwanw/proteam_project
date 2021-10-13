using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceProfile.Component;
using ServiceProfile.Data;
using ServiceProfile.Models;
using ServiceProfile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceProfile.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private ILookup _lookup;
        public ProteamContext _context;

        public DashboardController(ILookup lookup, ProteamContext context)
        {
            _context = context;
            _lookup = lookup;

        }
        // GET: api/<DashboardController>
        [HttpGet("GetVendor")]
        public async Task<IActionResult> GetVendor()
        {

            try
            {
                DashboardVendor dashboardvendor = new DashboardVendor();
                int TotalMandays = 0;
                int TotalVendor = 0;
                List<VendorMandays> ListVendor = new List<VendorMandays>();

                TotalVendor = StoredProcedureExecutor.ExecuteScalarSP<int>(_context, "count_mandays", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")

                });
                TotalMandays = StoredProcedureExecutor.ExecuteScalarSP<int>(_context, "count_mandays", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")

                });
                ListVendor = StoredProcedureExecutor.ExecuteSPList<VendorMandays>(_context, "count_mandays", new SqlParameter[]{
                        new SqlParameter("@Flag", 4),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });
                dashboardvendor.TotalVendor = TotalVendor;
                dashboardvendor.TotalMandays = TotalMandays;
                dashboardvendor.ListVendors = ListVendor;

                return Ok(new { dashboardvendor });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _lookup.GetAll();

        }

        [HttpGet("GetResource")]
        public async Task<IActionResult> GetResource()
        {

            try
            {
                DashboardResources dashboardresources = new DashboardResources();
                int TotalResource = 0;
                List<ResourceKelompok> ListResourceKelompok = new List<ResourceKelompok>();
                List<ResourceRole> ListResourceRole = new List<ResourceRole>();
                List<ResourceType> ListResourceType = new List<ResourceType>();

                TotalResource = StoredProcedureExecutor.ExecuteScalarSP<int>(_context, "count_resources", new SqlParameter[]{
                        new SqlParameter("@Flag", 1),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")

                });
                ListResourceKelompok = StoredProcedureExecutor.ExecuteSPList<ResourceKelompok>(_context, "count_resources", new SqlParameter[]{
                        new SqlParameter("@Flag", 2),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });
                ListResourceRole = StoredProcedureExecutor.ExecuteSPList<ResourceRole>(_context, "count_resources", new SqlParameter[]{
                        new SqlParameter("@Flag", 3),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });
                ListResourceType = StoredProcedureExecutor.ExecuteSPList<ResourceType>(_context, "count_resources", new SqlParameter[]{
                        new SqlParameter("@Flag", 4),
                        new SqlParameter("@Parameter1", "Test"),
                        new SqlParameter("@Parameter2", "Test")
                });
                foreach(ResourceType rt in ListResourceType)
                {
                    decimal? persendata = Math.Round(Convert.ToDecimal(rt.TotalResourceType) / Convert.ToInt32(TotalResource) * 100, 2);
                    rt.PercentageRecourceType = Convert.ToString(persendata) + " %";
                }
                dashboardresources.TotalResource = TotalResource;

                dashboardresources.ListResourceKelompok = ListResourceKelompok;
                dashboardresources.ListResourceRole = ListResourceRole;
                dashboardresources.ListResourceType = ListResourceType;

                return Ok(new { dashboardresources });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }
            var results = await _lookup.GetAll();

        }

    }
}
