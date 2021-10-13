using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProTeam.ViewModel
{
    public class ResourceVModel
    {
        public int KelompokId { get; set; }
        public string KelompokName { get; set; }
        public int DivisiId { get; set; }
        public string DivisiName { get; set; }
        public int TotalEmployee { get; set; }
        public decimal TotalManhour { get; set; }
        public int Status { get; set; }
        public string nama_status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public string Skill { get; set; }
    }
}

