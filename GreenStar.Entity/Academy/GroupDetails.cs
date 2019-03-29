using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblGroupDetails")]
    public class GroupDetails : FullAuditingWithCreationDetails
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int ClassID { get; set; }
        public virtual ClassDetails classDetails { get; set; }        
    }
}
