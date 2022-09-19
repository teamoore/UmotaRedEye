using System.Runtime.Serialization;

namespace UmotaRedEye.Models
{
    [DataContract]
    public class ServiceResponse
    {
        [DataMember]
        public bool Success { get; private set; }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public object? Value { get; private set; }

        public ServiceResponse(ResponseType responseType, string message, object value = null)
        {
            this.Success = responseType == ResponseType.Success;
            this.Message = message;                
            this.Value = value;

        }
    }

    public enum ResponseType
    {
        Success = 1,
        Error = 2
    }
}
