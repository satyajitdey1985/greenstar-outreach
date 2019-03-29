using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblPerformanceCount")]
    public class PerformanceCount : FullAuditingWithCreationDetails
    {
        [Key]
        public int PerformID { get; set; }
        public int ParamID { get; set; }
        public virtual ParameterAttribute param { get; set; }
        public int studID { get; set; }
        public virtual Student student { get; set; }
        public DateTime ParamDate { get; set; }
        public bool Point { get; set; }
    }
}
