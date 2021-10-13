using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProfile.Models
{
    public partial class DataUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Npp { get; set; }
        //public string Token { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Role { get; set; }
        public int? KelompokId { get; set; }
        public int? Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
