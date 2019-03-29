using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.User
{
    [Table("tblRole")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
