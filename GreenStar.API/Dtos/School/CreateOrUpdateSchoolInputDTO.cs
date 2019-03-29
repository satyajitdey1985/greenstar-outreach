using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos.School
{
   
    public class CreateOrUpdateSchoolInputDTO
    {
       [Required(ErrorMessage = "Name is required")]
        public string schoolName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string schlAddress1 { get; set; }
        public string schlAddress2 { get; set; }
        public string schlAddress3 { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public string schlZip { get; set; }
        [Required(ErrorMessage = "Phone no is required")]
        public string schlTeleNo { get; set; }
        public string schlMobileNo { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string schlMailID { get; set; }
        public string schlPOCName { get; set; }
        public string schlLongitude { get; set; }
        public string schlLatitude { get; set; }      
        public int CityID { get; set; }      
        public int? CreatedBy { get; set; }       
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}