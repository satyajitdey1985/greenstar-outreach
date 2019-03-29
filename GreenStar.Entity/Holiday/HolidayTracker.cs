using GreenStar.Entity.Academy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenStar.Entity.Holiday
{
    [Table("tblHolidayTrackers")]
    public class HolidayTracker
    {
        [Key]
        public int HolidayTrackID { get; set; }
        //[ForeignKey("School")]
        public int schlID { get; set; }
        public virtual School school { get; set; }
        //[ForeignKey("HolidayCalendar")]
        public int HolidayID { get; set; }
        public virtual HolidayCalendar holidayCalendar { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
