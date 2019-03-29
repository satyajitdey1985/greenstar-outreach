using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos.School
{
    public class SchoolListDTO
    {
        public int schlID { get; set; }
        public string schoolName { get; set; }       
        public string schlAddress1 { get; set; }
        public string schlAddress2 { get; set; }
        public string schlAddress3 { get; set; }       
        public string schlZip { get; set; }
        public string schlTeleNo { get; set; }
        public string schlMobileNo { get; set; }        
        public string schlMailID { get; set; }
        public string schlPOCName { get; set; }
        public string schlLongitude { get; set; }
        public string schlLatitude { get; set; }
        public int cityId { get; set; }
        public City city { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }


    }
}