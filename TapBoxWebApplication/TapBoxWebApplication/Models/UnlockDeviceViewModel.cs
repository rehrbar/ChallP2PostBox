using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TapBoxWebApplication.Models {
    public class UnlockDeviceViewModel
    {
        public string DeviceName;
        public DateTime Requested = DateTime.Now;
    }
}