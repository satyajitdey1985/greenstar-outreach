using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenStar.Entity.Holiday
{
    [Table("tblHolidayCalendars")]
    public class HolidayCalendar
    {
        [Key]
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