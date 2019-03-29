using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class CreateOrUpdateGroupStudentMappingDto
    {

        public int GroupStudentMappingID { get; set; }
        public int GroupID { get; set; }
        public int studID { get; set; }
        public bool IsActive { get; set; }
        
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }       

        public DateTime CreatioinDate { get; set; }
       
        public int ModifyBy { get; set; }                   

        public DateTime ModifyDate { get; set; }
        
    }
}