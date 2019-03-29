using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class GroupDetailsListDTO
    {       
        public string GroupName { get; set; }
        public ClassDetails classDetails { get; set; }
    }
}