using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Address
{
    [Table("tblCities")]
    public class City
    {
        [Key]       
        public int CityID { get; set; }
        public string CityName { get; set; }
        //[ForeignKey("State")]
        public int statID { get; set; }
        public virtual State state { get; set; }
    }
}
