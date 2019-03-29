using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblStudentDetails")]
    public class Student : FullAuditingWithCreationDetails
    {
        [Key]
        public int studID { get; set; }
        public string studName { get; set; }
        public int RollNo { get; set; }
        public int ClassID { get; set; }
        public virtual ClassDetails classDetails { get; set; }
        public string studAddress1 { get; set; }
        public string studAddress2 { get; set; }
        public string studAddress3 { get; set; }
        public string studlZip { get; set; }
        public string studMobileNo { get; set; }
        public string schlMailID { get; set; }
        public bool isAssignToGroup { get; set; }
    }
}
