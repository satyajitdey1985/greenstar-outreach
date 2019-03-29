using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class HolidayCalendarDto
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HolidayID { get; set; }

        [Required]
        public DateTime HolidayDate { get; set; }

        [Required]
        public string HolidayName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatioinDate { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}