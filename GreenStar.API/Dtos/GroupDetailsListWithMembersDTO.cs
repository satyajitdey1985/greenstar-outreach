using GreenStar.Entity.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenStar.API.Dtos
{
    public class GroupDetailsListWithMembersDTO
    {
        //public int groupID { get; set; }
        //public string GroupName { get; set; }        
        public GroupDetails group { get; set; }
        public List<Student> members { get; set; }
    }
}