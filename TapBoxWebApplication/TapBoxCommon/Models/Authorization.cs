using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapBoxCommon.Models {
    public class Authorization {
        [Key]
        [Column(Order=1)]
        public Guid AccessKeyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string DeviceName { get; set; }

        public virtual AccessKey Key { get; set; }
        public virtual Device Device { get; set; }
    }
}
