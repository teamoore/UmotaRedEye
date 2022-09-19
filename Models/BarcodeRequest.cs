using System.Runtime.Serialization;

namespace UmotaRedEye.Models
{
    [DataContract]
    public class BarcodeRequest
    {
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public string Barcode { get; set; }
    }
}
