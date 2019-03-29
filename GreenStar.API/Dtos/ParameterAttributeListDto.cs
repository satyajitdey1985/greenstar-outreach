using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class ParameterAttributeListDto
    {
        public int ParamID { get; set; }
        public string ParamName { get; set; }
        public decimal RedVal { get; set; }
        public decimal GreenVal { get; set; }
        public decimal YellowVal { get; set; }
        public string Comments { get; set; }        
    }
}