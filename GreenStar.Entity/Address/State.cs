using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Address
{
    [Table("tblStates")]
    public class State
    {
        [Key]
        public int statID { get; set; }
        public string statName { get; set; }
    }
}
