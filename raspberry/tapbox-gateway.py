
import time
import sys
import iothub_client
import re
from iothub_client import *

# HTTP options
# Because it can poll "after 9 seconds" polls will happen effectively
# at ~10 seconds.
# Note that for scalabilty, the default value of minimumPollingTime
# is 25 minutes. For more information, see:
# https://azure.microsoft.com/documentation/articles/iot-hub-devguide/#messaging
timeout = 241000
minimum_polling_time = 9

# messageTimeout - the maximum time in milliseconds until a message times out.
# The timeout period starts at IoTHubClient.send_event_async. 
# By default, messages do not expire.
message_timeout = 10000

# chose HTTP, AMQP or MQTT as transport protocol
protocol = IoTHubTransportProvider.AMQP
#protocol = IoTHubTransportProvider.HTTP
#protocol = IoTHubTransportProvider.MQTT

receive_context = 0

# String containing Hostname, Device Id & Device Key in the format:
# "HostName=<host_name>;DeviceId=<device_id>;SharedAccessKey=<device_key>"
#connection_string = "HostName=<host_name>;DeviceId=<device_id>;SharedAccessKey=<device_key>"
connection_string = "HostName=ChallengeProjFree.azure-devices.net;DeviceId=Montag123;SharedAccessKey=zXN1bSoMSfaA/7FsUegHL8NRvCaxTJsiq6hI4IJidgU="
#connection_string = sys.argv[1]
print("Connection string: %s" % connection_string)
m = re.search(r"DeviceId=(\w*);", connection_string, re.IGNORECASE)
if not m:
    print("Connection string does not contain deviceId.")
    sys.exit(1)
deviceId = m.group(1)

msg_txt = "{\"deviceId\": \"%s\",\"cardUid\": %s}"

def receive_message_callback(message, counter):
    buffer = message.get_bytearray()
    size = len(buffer)
    print("Received Message [%d]:" % counter)
    print("    Data: <<<%s>>> & Size=%d" % (buffer[:size].decode('utf-8'), size))
    map_properties = message.properties()
    key_value_pair = map_properties.get_internals()
    print("    Properties: %s" % key_value_pair)
    counter += 1
    return IoTHubMessageDispositionResult.ACCEPTED

def send_confirmation_callback(message, result, user_context):
    print(
        "Confirmation[%d] received for message with result = %s" %
        (user_context, result))
    map_properties = message.properties()
    print("    message_id: %s" % message.message_id)
    print("    correlation_id: %s" % message.correlation_id)
    key_value_pair = map_properties.get_internals()
    print("    Properties: %s" % key_value_pair)

    

def iothub_client_init():
    # prepare iothub client
    iotHubClient = IoTHubClient(connection_string, protocol)
    if iotHubClient.protocol == IoTHubTransportProvider.HTTP:
        iotHubClient.set_option("timeout", timeout)
        iotHubClient.set_option("MinimumPollingTime", minimum_polling_time)
    # set the time until a message times out
    iotHubClient.set_option("messageTimeout", message_timeout)
    # to enable MQTT logging set to 1
    if iotHubClient.protocol == IoTHubTransportProvider.MQTT:
        iotHubClient.set_option("logtrace", 0)
    iotHubClient.set_message_callback(
        receive_message_callback, receive_context)
    return iotHubClient
    
def iothub_client_sample_run():

    try:

        iotHubClient = iothub_client_init()

        while True:
            i = 1337
            msg_txt_formatted = msg_txt % (deviceId, "112233")
            message = IoTHubMessage(msg_txt_formatted)
            # optional: assign ids
            message.message_id = "message_%d" % i
            message.correlation_id = "correlation_%d" % i
            # optional: assign properties
            prop_map = message.properties()
            prop_text = "PropMsg_%d" % i
            prop_map.add("Property", prop_text)
            iotHubClient.send_event_async(message, send_confirmation_callback, i)
            print(
                "IoTHubClient.send_event_async accepted message [%d]"
                " for transmission to IoT Hub." %
                i)

            # Wait for Commands or exit
            print("IoTHubClient waiting for commands, press Ctrl-C to exit")

            n = 0
            while n < 6:
                status = iotHubClient.get_send_status()
                print("Send status: %s" % status)
                time.sleep(10)
                n += 1

    except IoTHubError as e:
        print("Unexpected error %s from IoTHub" % e)
        return
    except KeyboardInterrupt:
        print("IoTHubClient sample stopped")
        
if __name__ == '__main__':
    print("\nPython %s" % sys.version)
    print("IoT Hub for Python SDK Version: %s" % iothub_client.__version__)

    print("Starting the IoT Hub Python sample...")
    print("    Protocol %s" % protocol)
    print("    Connection string=%s" % connection_string)

    iothub_client_sample_run()