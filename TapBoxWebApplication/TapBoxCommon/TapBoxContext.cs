using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapBoxCommon.Models;

namespace TapBoxCommon
{
    public class TapBoxContext : DbContext
    {
        public TapBoxContext() : base("name=TapBoxContext") { }
        public TapBoxContext(string connectionString) : base(connectionString) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }
    }
}
