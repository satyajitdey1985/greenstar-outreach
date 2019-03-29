using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class GroupStudentMappingListDto
    {
        public GroupDetails groupDetails { get; set; }        
        public Student student { get; set; }
    }
}