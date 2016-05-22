using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace TapBoxWebjob {
    public class Functions
    {
        public const string EventHubName = "iothub-ehub-challengep-36513-6fd2520fa8";
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log) {
            log.WriteLine(message);
        }
        
        public static void ProcessDeviceMessage([EventHubTrigger(EventHubName)] string data, TextWriter log)
        {
            log.WriteLine(data);
            log.WriteLine($"Hallo Welt {DateTime.Now}");
        }
    }
}
