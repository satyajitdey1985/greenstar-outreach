using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class CreateGroupDetailsWithStudentsDTO
    {
        [Required(ErrorMessage = "Group's Name is required.")]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Group's Name is required.")]
        public int ClassID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatioinDate { get; set; }
        public List<int> studentIDs { get; set; }       
    }
}