using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TapBoxCommon.Models {
    public class AccessKey
    {
        public Guid Id { get; set; }
        [Required]
        public string CardUID { get; set; }
        public DateTime? LastAccessed { get; set; }
        [Required]
        public string OwnerEmail { get; set; }

        public virtual IEnumerable<Authorization> Authorizations { get; set; }
    }
}
