using System;
using System.Threading;
using Microsoft.Azure;
using TapBoxCommon;
using System.Threading.Tasks;
using System.Linq;
using TapBoxCommon.Models;

namespace TapBoxWebjob
{
    public class Program
    {
        private static void Main()
        {
            var client = new ReceiverClient(CloudConfigurationManager.GetSetting("AzureIoTHub.ConnectionString"));
            var command = new Commands(CloudConfigurationManager.GetSetting("AzureIoTHub.ConnectionString"));
            var dbContext = new TapBoxContext();
            client.OnNewAccessKeyMessageEvent += async (id, uid) =>
            {
                Console.WriteLine($"Device: {id} - UID: {uid}");
                // TODO check against database and send c2d message if it's allowed.
                if (uid == "4511E552")
                {
                    Console.WriteLine("Lucky guess? - Device shall be unlocked, your Majesty!");
                    await command.SendUnlockAsync(id);
                }
            };

            client.OnNewReadWeightSensorEvent += (DeviceId, WeightSensorValue) =>
            {
                Device dev = dbContext.Devices.Find(DeviceId);
                MailSender.SendMailToOwner(dev, WeightSensorValue);
            };

            client.ReceiveMessages();
            Console.WriteLine("Press return to shut down.");
            Console.ReadLine();
            client.CancelReceiving();
            Thread.Sleep(200);
        }
    }
}