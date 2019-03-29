using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity.Academy
{
    [Table("tblParameterAttibute")]
    public class ParameterAttribute : FullAuditingWithCreationDetails
    {
        [Key]
        public int ParamID { get; set; }
        [Required (ErrorMessage ="Parameter Name is required")]
        public string ParamName { get; set; }
        [Required (ErrorMessage ="Please enter percentage of Upper limit of Red Value")]
        public decimal RedVal { get; set; }
        [Required(ErrorMessage = "Please enter percentage of Upper limit of Green Value")]
        public decimal GreenVal { get; set; }
        [Required(ErrorMessage = "Please enter percentage of Upper limit of Yellow Value")]
        public decimal YellowVal { get; set; }
        public string Comments { get; set; }        
    }
}
