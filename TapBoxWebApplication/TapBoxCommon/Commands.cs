using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace TapBoxCommon {
    public class Commands
    {
        private readonly ServiceClient _serviceClient;

        public Commands(string connectionString)
        {
            _serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        /// <summary>
        ///     Sends a c2d message through IoT Hub to the device.
        /// </summary>
        /// <param name="identity">Device id of the receiver.</param>
        public async Task SendUnlockAsync(string identity) {
            var commandMessage = new Message(Encoding.UTF8.GetBytes("OpenLock"));
            await _serviceClient.SendAsync(identity, commandMessage);
        }
    }
}
