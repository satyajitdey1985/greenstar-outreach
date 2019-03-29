using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblClassDetails")]
    public class ClassDetails
    {
        [Key]
        public int ClassID { get; set; }
        public int schlID { get; set; }         
        public virtual School school { get; set; }
        public int classStandard { get; set; }
        public string sectName { get; set; }
        public int  sectStrength { get; set; }
        public string  sectPOCName { get; set; }
        public string  sectPOCEmailID { get; set; }
        public string  sectPOCContactNo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
