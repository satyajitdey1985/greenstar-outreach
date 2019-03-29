using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos.StudentDto
{
    public class CreateOrUpdateStudentInputDTO
    {
        public int studID { get; set; }
        [Required(ErrorMessage = "Student's Name is required.")]
        public string studName { get; set; }
        [Required(ErrorMessage = "Student's Roll Number is required.")]
        public int RollNo { get; set; }
        [Required(ErrorMessage = "Student's Class is required.")]
        public int ClassID { get; set; }
        public string studAddress1 { get; set; }
        public string studAddress2 { get; set; }
        public string studAddress3 { get; set; }
        public string studlZip { get; set; }
        public string studMobileNo { get; set; }
        [EmailAddress]
        public string schlMailID { get; set; }
        public bool isAssignToGroup { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatioinDate { get; set; }

        public int ModifyBy { get; set; }

        public DateTime ModifyDate { get; set; }

        public bool IsActive { get; set; }


    }
}