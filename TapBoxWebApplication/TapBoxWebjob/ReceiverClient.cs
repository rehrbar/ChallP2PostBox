using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using TapBoxWebjob.Models;

namespace TapBoxWebjob
{
    /// <summary>
    ///     Represents a client like it's used as application backend.
    ///     This client was inspired by
    ///     <see href="https://azure.microsoft.com/en-us/documentation/articles/iot-hub-csharp-csharp-getstarted/">
    ///         Azure Documentation IoT Getting Started
    ///     </see>
    /// </summary>
    public class ReceiverClient
    {
        /// <summary>
        ///     Delegate which gets called if a new AccessKeyMessage is received.
        /// </summary>
        /// <param name="deviceId">Device id of the source.</param>
        /// <param name="cardUid">Measured value of the device.</param>
        public delegate void NewAccessKeyMessageEventHandler(string deviceId, string cardUid);
        
        /// <summary>
        ///     Delegate which gets called if a new message is received.
        /// </summary>
        /// <param name="deviceId">Device id of the source.</param>
        /// <param name="sensorValue">Measured sensor Value.</param>
        public delegate void NewReadWeightSensorEventHandler(string deviceId, int sensorValue);

        /// <summary>
        ///     Name of the event queue.
        /// </summary>
        public const string IotHubD2CEndpoint = "messages/events";

        private readonly EventHubClient _eventHubClient;
        private readonly ServiceClient _serviceClient;
        private CancellationTokenSource _cts;

        /// <summary>
        ///     Initializes the <see cref="ReceiverClient" /> classs.
        /// </summary>
        /// <param name="connectionString"></param>
        public ReceiverClient(string connectionString)
        {
            _eventHubClient =
                EventHubClient.CreateFromConnectionString(
                    connectionString, IotHubD2CEndpoint);
            _serviceClient =
                ServiceClient.CreateFromConnectionString(
                    connectionString);
        }

        /// <summary>
        ///     Occurs after a new d2c message was received.
        /// </summary>
        public event NewAccessKeyMessageEventHandler OnNewAccessKeyMessageEvent;
        /// <summary>
        ///     Occurs after a new d2c message was received.
        /// </summary>
        public event NewReadWeightSensorEventHandler OnNewReadWeightSensorEvent;

        /// <summary>
        ///     Starts tasks to recieve d2c messages in background on all partitions.
        /// </summary>
        public void ReceiveMessages()
        {
            var d2cPartitions = _eventHubClient.GetRuntimeInformation().PartitionIds;

            _cts = new CancellationTokenSource();

            foreach (var partition in d2cPartitions)
            {
                Task.Run(() => ReceiveMessagesFromDeviceAsync(partition, _cts.Token));
            }
        }

        /// <summary>
        ///     Shuts down the receiver.
        /// </summary>
        public void CancelReceiving()
        {
            _cts.Cancel();
        }

        /// <summary>
        ///     Sends a c2d message through IoT Hub to the device.
        /// </summary>
        /// <param name="identity">Device id of the receiver.</param>
        public async Task SendC2DMessageAsync(string identity)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            await _serviceClient.SendAsync(identity, commandMessage);
        }

        /// <summary>
        ///     Receives a d2c message from a single partition.
        /// </summary>
        /// <param name="partition">Partition id to listen to.</param>
        /// <param name="ct">Token to cancel the receive process.</param>
        private async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            // The Event Hubs-compatible endpoint for reading device-to-cloud messages always uses the AMQPS protocol.
            var eventHubReceiver = _eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                var eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                var data = Encoding.UTF8.GetString(eventData.GetBytes());
                // TODO catch if messageType not available
                
                if (eventData.Properties["messageType"].Equals("accessKey")) {
                    HandleUnlockRequest(data);
                } else if (eventData.Properties["messageType"].Equals("weightSensor")) {
                    HandleWeightSensorMessage(data);
                }
                   
            }
        }

        private void HandleWeightSensorMessage(string rawData)
        {
            var dto = JsonConvert.DeserializeObject<ReadWeightSensorDTO>(rawData);
            OnNewReadWeightSensorEvent?.Invoke(dto.DeviceId, dto.SensorValue);
        }

        private void HandleUnlockRequest(string rawData)
        {
            var dto = JsonConvert.DeserializeObject<AccessKeyMessageDTO>(rawData);
            OnNewAccessKeyMessageEvent?.Invoke(dto.DeviceId, dto.CardUid);
        }
    }
}