using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class PerformanceCountListDto
    {
        [Key]
        public int PerformID { get; set; }
        public int ParamID { get; set; }
        public int studID { get; set; }
        public DateTime ParamDate { get; set; }
    }
}