using System;
using System.Threading;
using Microsoft.Azure;

namespace TapBoxWebjob
{
    public class Program
    {
        private static void Main()
        {
            var client = new ReceiverClient(CloudConfigurationManager.GetSetting("AzureIoTHub.ConnectionString"));

            client.OnNewMessageEvent += (id, uid) =>
            {
                Console.WriteLine($"Device: {id} - UID: {uid}");
                // TODO check against database and send c2d message if it's allowed.
            };

            client.ReceiveMessages();
            Console.WriteLine("Press return to shut down.");
            Console.ReadLine();
            client.CancelReceiving();
            Thread.Sleep(200);
        }
    }
}