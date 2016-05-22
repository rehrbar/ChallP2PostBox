using System;
using System.Threading;
using Microsoft.Azure;
using TapBoxCommon;

namespace TapBoxWebjob
{
    public class Program
    {
        private static void Main()
        {
            var client = new ReceiverClient(CloudConfigurationManager.GetSetting("AzureIoTHub.ConnectionString"));
            var command = new Commands(CloudConfigurationManager.GetSetting("AzureIoTHub.ConnectionString"));

            client.OnNewMessageEvent += (id, uid) =>
            {
                Console.WriteLine($"Device: {id} - UID: {uid}");
                // TODO check against database and send c2d message if it's allowed.
                if (uid == "112233")
                {
                    Console.WriteLine("Lucky guess? - Device shall be unlocked, your Majesty!");
                    command.SendUnlockAsync(id);
                }
            };

            client.ReceiveMessages();
            Console.WriteLine("Press return to shut down.");
            Console.ReadLine();
            client.CancelReceiving();
            Thread.Sleep(200);
        }
    }
}