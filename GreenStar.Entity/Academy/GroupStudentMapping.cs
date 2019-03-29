using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblGroupStudentMapping")]
    public class GroupStudentMapping : FullAuditingWithCreationDetails
    {
        [Key]
        public int GroupStudentMappingID { get; set; }
        public int GroupID { get; set; }
        public virtual GroupDetails groupDetails { get; set; }

        public int studID { get; set; }
        public virtual Student student { get; set; }
    }
}
