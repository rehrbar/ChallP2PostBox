using System;
using System.Linq;
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
            client.OnNewAccessKeyMessageEvent += async (id, uid) =>
            {
                Console.WriteLine($"Device: {id} - UID: {uid}");
                using (var dbContext = new TapBoxContext())
                {
                    var authorization =
                        dbContext.Authorizations.FirstOrDefault(
                            a => a.DeviceName.Equals(id) && a.Key.CardUID.Equals(uid));
                    // TODO check against database and send c2d message if it's allowed.
                    if (authorization == null)
                    {
                        Console.WriteLine("Key/Device does not match.");
                        return;
                    }
                    Console.WriteLine("Lucky guess? - Device shall be unlocked, your Majesty!");
                    await command.SendUnlockAsync(id);
                }
            };

            client.OnNewReadWeightSensorEvent += (deviceId, weightSensorValue) =>
            {
                Console.WriteLine($"Device: {deviceId} - WeightSensorValue: {weightSensorValue}");
                using (var dbContext = new TapBoxContext())
                {
                    var dev = dbContext.Devices.Find(deviceId);
                    MailSender.SendMailToOwner(dev, weightSensorValue);
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