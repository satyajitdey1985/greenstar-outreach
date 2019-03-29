using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.Entity
{
    public class FullAuditingWithCreationDetails
    {
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        private DateTime? creatioinDate = null;

        public DateTime CreatioinDate
        {
            get
            {
                return creatioinDate.HasValue
                   ? creatioinDate.Value
                   : DateTime.UtcNow;
            }

            set { this.creatioinDate = value; }
        }
        public int ModifyBy { get; set; }


        private DateTime? modifyDate = null;

        public DateTime ModifyDate
        {
            get
            {
                return modifyDate.HasValue
                   ? modifyDate.Value
                   : DateTime.UtcNow;
            }

            set { this.modifyDate = value; }
        }
    }
}
