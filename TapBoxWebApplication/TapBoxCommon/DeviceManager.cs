using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace TapBoxCommon
{
    /// <summary>
    ///     Represents a manager for the IoT Hub to handle the devices.
    /// </summary>
    public class DeviceManager
    {
        private readonly RegistryManager _registryManager;

        /// <summary>
        ///     Initializes the <see cref="DeviceManager" /> classs.
        /// </summary>
        public DeviceManager(string connectionString)
        {
            _registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            // ConfigurationManager.AppSettings["AzureIoTHub.ConnectionString"]
        }

        /// <summary>
        ///     Adds a device to the IoT Hhub and retreies it's primary symmetric key.
        /// </summary>
        /// <param name="deviceId">Device id of the device to register.</param>
        /// <returns>Primary symmetric key of the device.</returns>
        public async Task<string> AddDeviceAsync(string deviceId)
        {
            Device device;
            try
            {
                device = await _registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                // Handle already existing devices.
                device = await _registryManager.GetDeviceAsync(deviceId);
            }
            return device.Authentication.SymmetricKey.PrimaryKey;
        }

        /// <summary>
        ///     Removes a device of the IoT Hub.
        /// </summary>
        /// <param name="deviceId">Device id of the device to remove.</param>
        public async Task RemoveDeviceAsync(string deviceId)
        {
            await _registryManager.RemoveDeviceAsync(deviceId);
        }
    }
}