
import time
import sys
import iothub_client
import re
import serial
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

# Serial communication
fsrRegexPattern = re.compile("^fsrValue:(\d+)\r$")
cardUidRegexPattern = re.compile("^CardUID:([0-9a-fA-F]+)\r$")
ser = serial.Serial("/dev/ttyACM0", 9600)

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

key_msg_txt = "{\"deviceId\": \"%s\",\"cardUid\": %s}"
fsr_msg_txt = "{\"deviceId\": \"%s\",\"sensorValue\": %d}"

def receive_message_callback(message, counter):
    buffer = message.get_bytearray()
    size = len(buffer)
    print("Received Message [%d]:" % counter)
    print("    Data: <<<%s>>> & Size=%d" % (buffer[:size].decode('utf-8'), size))
    map_properties = message.properties()
    key_value_pair = map_properties.get_internals()
    print("    Properties: %s" % key_value_pair)
    counter += 1
    # TODO check if server message is really unlock.
    ser.write(b"unlock\n")
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
    
def iothub_client_run():

    try:

        iotHubClient = iothub_client_init()
        print("IoTHubClient running, press Ctrl-C to exit")

        while True:
            # Wait for Commands or exit
            serialData = ser.readline().decode("ASCII") 
            print("Serial data: " + serialData)
            groups = fsrRegexPattern.search(serialData)
            if groups is not None:
                fsr = groups.group(1)
                print("-> Sensor value: " + fsr)
                
                message = IoTHubMessage(fsr_msg_txt % (deviceId, int(fsr)) )
                prop_map = message.properties()
                prop_map.add("messageType", "weightSensor")
                iotHubClient.send_event_async(message, send_confirmation_callback, 1234)
            else:
                print("Not a sensor value")
                
            groups = cardUidRegexPattern.search(serialData)
            if groups is not None:
                uid = groups.group(1)
                print("-> Card UID: " + uid)
                
                message = IoTHubMessage(key_msg_txt % (deviceId, uid) )
                prop_map = message.properties()
                prop_map.add("messageType", "accessKey")
                iotHubClient.send_event_async(message, send_confirmation_callback, 3333)
            else:
                print("Not a key card")

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

    iothub_client_run()