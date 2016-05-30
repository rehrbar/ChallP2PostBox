using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SendGrid;
using TapBoxCommon.Models;
using Microsoft.Azure;

namespace TapBoxWebjob
{
    class MailSender
    {
        public static void SendMailToOwner(Device NotifierDevice, int SensorValue)
        {

            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(NotifierDevice.OwnerMailAddress);
            myMessage.From = new MailAddress("sjost@hsr.ch", "Samuel Jost");
            myMessage.Subject = "Notification from your Tapbox";

            Console.WriteLine($"Device: {NotifierDevice.DeviceName} - Sensor Value: {SensorValue}");
            if (SensorValue < 100)
            {
                Console.WriteLine("Your Mailbox is Empty");
                return;
            }
            else if (SensorValue < 300)
            {
                Console.WriteLine("You have some Mail");
                myMessage.Text = "You have some Mail in your device "+NotifierDevice.DeviceName;
            }
            else if (SensorValue > 300)
            {
                Console.WriteLine("You have A Lot of Mail");
                myMessage.Text = "You have a lot of Mail in your device " + NotifierDevice.DeviceName;
            }

            var apiKey = CloudConfigurationManager.GetSetting("SendGrid.API_Key");
            var transportWeb = new Web(apiKey);

            // Send the email, which returns an awaitable task.
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
