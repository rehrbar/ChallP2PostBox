using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TapBoxCommon.Models {
    public class Device {
        [Key]
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public DateTime? LastContact { get; set; }
        public string OwnerMailAddress { get; set; }

        public virtual IEnumerable<Authorization> Authorizations { get; set; } 
    }
    
}
