using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class CreateOrUpdateParameterAttributeDto
    {
        //public int ParamID { get; set; }
        public string ParamName { get; set; }
        public decimal RedVal { get; set; }
        public decimal GreenVal { get; set; }
        public decimal YellowVal { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}