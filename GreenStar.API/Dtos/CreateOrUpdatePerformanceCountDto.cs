using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class CreateOrUpdatePerformanceCountDto
    {
        public int PerformID { get; set; }
        public int ParamID { get; set; }
        public int studID { get; set; }
        [Required(ErrorMessage = "Perform date is required")]
        public DateTime ParamDate { get; set; }        
        public bool Point { get; set; } = false;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}