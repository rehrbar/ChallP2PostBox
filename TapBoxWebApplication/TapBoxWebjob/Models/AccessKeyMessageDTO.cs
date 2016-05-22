using System.Runtime.Serialization;

namespace TapBoxWebjob.Models
{
    [DataContract]
    public class AccessKeyMessageDTO
    {
        [DataMember] public string CardUid;
        [DataMember] public string DeviceId;
    }
}