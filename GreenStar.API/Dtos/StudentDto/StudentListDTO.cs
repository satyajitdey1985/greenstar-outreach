using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos.StudentDto
{
    public class StudentListDTO
    {
        public int studID { get; set; }
        public string studName { get; set; }
        public int RollNo { get; set; }
        public int ClassID { get; set; }
        public string studAddress1 { get; set; }
        public string studAddress2 { get; set; }
        public string studAddress3 { get; set; }
        public string studlZip { get; set; }
        public string studMobileNo { get; set; }
        public string schlMailID { get; set; }
        public bool isAssignToGroup { get; set; }
    }
}