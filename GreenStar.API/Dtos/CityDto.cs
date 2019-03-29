using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class CityDto
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        //[ForeignKey("State")]
        public int statID { get; set; }
        public virtual State state { get; set; }
    }
}