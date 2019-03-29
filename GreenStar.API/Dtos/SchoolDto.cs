using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class SchoolDto
    {
        [Key]
        public int schlID { get; set; }
        public string SchoolName { get; set; }
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
        //[ForeignKey("City")]
        public int CityID { get; set; }
        public virtual City city { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}