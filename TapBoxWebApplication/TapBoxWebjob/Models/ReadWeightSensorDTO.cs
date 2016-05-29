using System.Runtime.Serialization;

namespace TapBoxWebjob.Models
{
    [DataContract]
    public class ReadWeightSensorDTO
    {
        [DataMember] public int SensorValue;
        [DataMember] public string DeviceId;
    }
}