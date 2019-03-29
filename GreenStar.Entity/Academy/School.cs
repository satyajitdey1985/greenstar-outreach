using GreenStar.Entity.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblSchools")]
    public class School: FullAuditingWithCreationDetails
    {
        [Key]
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
        //[ForeignKey("City")]
        public int cityId { get; set; }
        public virtual City city { get; set; }
       


    }
}
